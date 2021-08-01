using System.Threading;
using System.Threading.Tasks;
using Application.Flows.Assets.Commands;
using Application.Models;
using Application.Request;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Application.IntegrationTests.Flows.Assets.Commands
{
    public class CreateAssetHandlerShould : IClassFixture<ServicesFixture>
    {
        private readonly IRequestHandler<CreateAssetCommand, AssetObjectModel> _handler;

        public CreateAssetHandlerShould(ServicesFixture fixture)
        {
            _handler = fixture.ServiceProvider.GetRequiredService<IRequestHandler<CreateAssetCommand, AssetObjectModel>>();
        }

        [Fact]
        public async Task ResponseEqualsToObjectModel()
        {
            var command = new Faker<CreateAssetCommand>()
                .RuleFor(r => r.AssetName, f => f.Random.String(2, 15))
                .RuleFor(r => r.AssetOwner, f => f.Random.String(2, 15))
                .RuleFor(r => r.QuadKey, f => f.Random.String(12))
                .Generate();

            var objectModel = new AssetObjectModel {AssetName = command.AssetName, AssetOwner = command.AssetOwner, QuadKey = command.QuadKey};

            var result = await _handler.HandleAsync(command, CancellationToken.None);

            result.Should().BeOfType<AssetObjectModel>().And.BeEquivalentTo(objectModel, options => options.Excluding(e => e.AssetId));
        }
    }
}