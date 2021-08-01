using System.Text.Json;
using Application.Flows.Assets.Commands;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Flows.Assets.Commands
{
    public class CreateAssetCommandShould
    {
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(12))
                .Generate();

            command.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(command));
        }
    }
}