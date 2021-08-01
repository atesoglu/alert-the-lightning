using System.Text.Json;
using Application.Flows.LightningStrikes.Commands;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeCommandShould
    {
        [Fact]
        public void ToStringSerializedAsJson()
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

            command.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(command));
        }
    }
}