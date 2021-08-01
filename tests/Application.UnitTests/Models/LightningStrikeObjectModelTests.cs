using System.Text.Json;
using Application.Models;
using Application.Models.Base;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models
{
    public class LightningStrikeObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new LightningStrikeObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new LightningStrikeObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<LightningStrikeModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<LightningStrikeModel>()
                .RuleFor(r => r.FlashType, f => f.PickRandom<FlashTypes>())
                .RuleFor(r => r.StrikedAt, f => f.Date.RecentOffset())
                .RuleFor(r => r.Latitude, f => f.Random.Double(-90, 90))
                .RuleFor(r => r.Longitude, f => f.Random.Double(-180, 180))
                .RuleFor(r => r.PeakAmps, f => f.Random.Int(-180, 180))
                .RuleFor(r => r.Reserved, f => f.Lorem.Word())
                .RuleFor(r => r.IcHeight, f => f.Random.Int(1, 180))
                .RuleFor(r => r.ReceivedAt, f => f.Date.RecentOffset())
                .RuleFor(r => r.NumberOfSensors, f => f.Random.Int(1, 180))
                .RuleFor(r => r.Multiplicity, f => f.Random.Int(1, 180))
                .Generate();

            var objectModel = new LightningStrikeObjectModel(domainModel);
            var assigned = new LightningStrikeObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.LightningStrikeId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<LightningStrikeObjectModel>()
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

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}