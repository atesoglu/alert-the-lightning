using System.Threading;
using System.Threading.Tasks;
using Application.Cache;
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
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.Flows.Assets.Commands
{
    public class CreateAssetHandler : IRequestHandler<CreateAssetCommand, AssetObjectModel>
    {
        private readonly IDataContext _dbContext;
        private readonly IValidator<CreateAssetCommand> _validator;
        private readonly IEventDispatcherService _eventDispatcherService;
        private readonly ILogger<CreateAssetHandler> _logger;

        public CreateAssetHandler(IDataContext dbContext, IValidator<CreateAssetCommand> validator, IEventDispatcherService eventDispatcherService, ILogger<CreateAssetHandler> logger)
        {
            _dbContext = dbContext;
            _validator = validator;
            _eventDispatcherService = eventDispatcherService;
            _logger = logger;
        }

        public async Task<AssetObjectModel> HandleAsync(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

            var model = await _dbContext.Assets.FirstOrDefaultAsync(w => w.QuadKey == request.QuadKey && w.AssetOwner == request.AssetOwner && w.AssetName == request.AssetName, cancellationToken);
            if (model != null) throw new BadRequestException($"Asset is already recorded in database with the AssetId: {model.AssetId}.");

            model = new AssetModel {AssetName = request.AssetName, AssetOwner = request.AssetOwner, QuadKey = request.QuadKey};

            await _dbContext.Assets.AddAsync(model, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            AssetTokenSourceProvider.ResetCancellationToken();

            var objectModel = new AssetObjectModel(model);
            await _eventDispatcherService.Dispatch(new AssetCreatedEvent(objectModel, request.RequestedAt), cancellationToken);

            return objectModel;
        }
    }
}