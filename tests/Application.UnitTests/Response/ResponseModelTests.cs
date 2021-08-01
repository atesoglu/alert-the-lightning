using System.Text.Json;
using Application.Response;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Response
{
    public class ResponseModelTests
    {
        [Fact]
        public void DefaultConstructorCreatesCorrelationId()
        {
            var actual = new ResponseModel();
            actual.CorrelationId.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void DefaultConstructorCreatesEmptyParams()
        {
            var actual = new ResponseModel();
            actual.Params.Should().BeEmpty();
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var model = new Faker<ResponseModel>()
                .RuleFor(r => r.Message, f => f.Lorem.Word())
                .Generate();

            model.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(model));
        }
    }
}