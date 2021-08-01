using Application.Flows.LightningStrikes.Commands;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Flows.LightningStrikes.Commands
{
    public class ProcessLightningStrikeValidatorShould
    {
        private readonly ProcessLightningStrikeValidator _validator;

        public ProcessLightningStrikeValidatorShould()
        {
            _validator = new ProcessLightningStrikeValidator();
        }

        [Fact]
        public void NotAllowZeroStrikeTime()
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
            command.StrikeTime = 0;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LatitudeMustBeGreaterThan()
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
            command.Latitude = -90.1;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LatitudeMustBeLessThan()
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
            command.Latitude = 90.1;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongitudeeMustBeGreaterThan()
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
            command.Longitude = -180.1;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void LongitudeMustBeLessThan()
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
            command.Longitude = 180.1;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NumberOfSensorsMustBeGreaterThanZero()
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
            command.NumberOfSensors = 0;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }
    }
}