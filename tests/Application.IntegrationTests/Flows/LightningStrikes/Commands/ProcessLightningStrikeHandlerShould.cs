using System.Threading;
using System.Threading.Tasks;
using Application.Flows.LightningStrikes.Commands;
using Application.Models;
using Application.Persistence;
using Application.Request;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.IntegrationTests.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeHandlerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel> _handler;

        public ProcessLightningStrikeHandlerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel>>();
            fixture.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        [Fact]
        public async Task ResponseEqualsToObjectModel()
        {
            var command = new Faker<ProcessLightningStrikeCommand>()
                .RuleFor(r => r.FlashType, f => f.PickRandom<FlashTypes>())
                .RuleFor(r => r.StrikeTime, f => f.Date.RecentOffset().ToUnixTimeMilliseconds())
                .RuleFor(r => r.Latitude, f => f.Random.Double(-90, 90))
                .RuleFor(r => r.Longitude, f => f.Random.Double(-180, 180))
                .RuleFor(r => r.PeakAmps, f => f.Random.Int(-180, 180))
                .RuleFor(r => r.Reserved, f => f.Lorem.Word())
                .RuleFor(r => r.IcHeight, f => f.Random.Int(1, 180))
                .RuleFor(r => r.ReceivedTime, f => f.Date.RecentOffset().ToUnixTimeMilliseconds())
                .RuleFor(r => r.NumberOfSensors, f => f.Random.Int(1, 180))
                .RuleFor(r => r.Multiplicity, f => f.Random.Int(1, 180))
                .Generate();

            var objectModel = new LightningStrikeObjectModel
            {
                FlashType = command.FlashType,
                StrikeTime = command.StrikeTime,
                Latitude = command.Latitude,
                Longitude = command.Longitude,
                PeakAmps = command.PeakAmps,
                Reserved = command.Reserved,
                IcHeight = command.IcHeight,
                ReceivedTime = command.ReceivedTime,
                NumberOfSensors = command.NumberOfSensors,
                Multiplicity = command.Multiplicity
            };

            var result = await _handler.HandleAsync(command, CancellationToken.None);

            result.Should().BeOfType<LightningStrikeObjectModel>().And.BeEquivalentTo(objectModel,
                options => options
                    .Excluding(e => e.LightningStrikeId)
                    .Excluding(e => e.StrikeTime)
                    .Excluding(e => e.ReceivedTime)
                    .Excluding(e => e.FlashTypeDescription)
            );
        }
    }
}