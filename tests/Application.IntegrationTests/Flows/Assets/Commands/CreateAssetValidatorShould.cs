using Application.Flows.Assets.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Flows.Assets.Commands
{
    public class CreateAssetValidatorShould
    {
        private readonly CreateAssetValidator _validator;

        public CreateAssetValidatorShould()
        {
            _validator = new CreateAssetValidator();
        }

        [Fact]
        public void NotAllowEmptyAssetOwner()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(13))
                .Generate();
            command.AssetOwner = string.Empty;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowEmptyAssetName()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(13))
                .Generate();
            command.AssetName = string.Empty;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowEmptyQuadKey()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(13))
                .Generate();
            command.QuadKey = string.Empty;

            _validator.Validate(command).IsValid.Should().BeFalse();
        }

        [Fact]
        public void NotAllowInvalidQuadKey()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(13))
                .Generate();

            _validator.Validate(command).IsValid.Should().BeFalse();
        }
    }
}