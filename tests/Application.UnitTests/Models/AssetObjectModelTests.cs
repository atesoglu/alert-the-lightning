using System.Text.Json;
using Application.Models;
using Application.Models.Base;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Models
{
    public class AssetObjectModelTests
    {
        [Fact]
        public void ExtendsFromObjectModelBase()
        {
            var actual = new AssetObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase>();
        }

        [Fact]
        public void ExtendsFromObjectModelBaseOfT()
        {
            var actual = new AssetObjectModel();
            actual.Should().BeAssignableTo<ObjectModelBase<AssetModel>>();
        }

        [Fact]
        public void AssignableFromDomainModel()
        {
            var domainModel = new Faker<AssetModel>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(12))
                .Generate();
            var objectModel = new AssetObjectModel {AssetName = domainModel.AssetName, AssetOwner = domainModel.AssetOwner, QuadKey = domainModel.QuadKey};

            var assigned = new AssetObjectModel(domainModel);

            assigned.Should().BeEquivalentTo(objectModel, options => options.Excluding(e => e.AssetId));
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var objectModel = new Faker<AssetObjectModel>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(12))
                .Generate();

            objectModel.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(objectModel));
        }
    }
}