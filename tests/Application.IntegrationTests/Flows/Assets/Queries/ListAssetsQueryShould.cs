using System.Text.Json;
using Application.Flows.Assets.Queries;
using FluentAssertions;
using Xunit;

namespace Application.IntegrationTests.Flows.Assets.Queries
{
    public class ListAssetsQueryShould
    {
        [Fact]
        public void ToStringSerializedAsJson()
        {
            var command = new ListAssetsQuery();
            command.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(command));
        }
    }
}