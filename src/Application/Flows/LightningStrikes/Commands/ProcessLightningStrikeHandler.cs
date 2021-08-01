using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Events;
using Application.Exceptions;
using Application.Models;
using Application.Persistence;
using Application.Request;
using Application.Services;
using Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeHandler : IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel>
    {
        private readonly IDataContext _dbContext;
        private readonly IValidator<ProcessLightningStrikeCommand> _validator;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly IAlertNotificationService _alertNotificationService;
        private readonly ILogger<ProcessLightningStrikeHandler> _logger;

        public ProcessLightningStrikeHandler(
            IDataContext dbContext,
            IValidator<ProcessLightningStrikeCommand> validator,
            IEventDispatcherService eventDispatcherService,
            IAlertNotificationService alertNotificationService,
            ILogger<ProcessLightningStrikeHandler> logger)
        {
            _dbContext = dbContext;
            _validator = validator;
            _eventDispatcherService = eventDispatcherService;
            _alertNotificationService = alertNotificationService;
            _logger = logger;
        }

        public async Task<LightningStrikeObjectModel> HandleAsync(ProcessLightningStrikeCommand request, CancellationToken cancellationToken)
        {
            if (request.FlashType == FlashTypes.Heartbeat) return new LightningStrikeObjectModel {FlashType = request.FlashType};

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult.Errors);

            var check = await _dbContext.LightningStrikes.FirstOrDefaultAsync(w => w.StrikedAt == DateTimeOffset.FromUnixTimeMilliseconds(request.StrikeTime)
                                                                                   && w.Longitude == request.Longitude
                                                                                   && w.Longitude == request.Longitude, cancellationToken);
            if (check != null) throw new BadRequestException($"Lightning strike is already recorded in database with the LightningStrikeId: {check.LightningStrikeId}.");

            var domainModel = new LightningStrikeModel
            {
                FlashType = request.FlashType,
                StrikedAt = DateTimeOffset.FromUnixTimeMilliseconds(request.StrikeTime),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                PeakAmps = request.PeakAmps,
                Reserved = request.Reserved,
                IcHeight = request.IcHeight,
                ReceivedAt = DateTimeOffset.FromUnixTimeMilliseconds(request.ReceivedTime),
                NumberOfSensors = request.NumberOfSensors,
                Multiplicity = request.Multiplicity
            };

            await _dbContext.LightningStrikes.AddAsync(domainModel, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var objectModel = new LightningStrikeObjectModel(domainModel);

            await _eventDispatcherService.Dispatch(new LightningStrikeCreatedEvent(objectModel, DateTimeOffset.Now), cancellationToken);
            await _alertNotificationService.ProcessAsync(objectModel, cancellationToken);

            return objectModel;
        }
    }
}