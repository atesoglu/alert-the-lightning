using System.Collections.Generic;
using System.Text.Json;
using Application.Response;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Response
{
    public class ErrorResponseModelTests
    {
        [Fact]
        public void DefaultConstructorCreatesCorrelationId()
        {
            var actual = new ErrorResponseModel();
            actual.CorrelationId.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void DefaultConstructorCreatesEmptyParams()
        {
            var actual = new ErrorResponseModel();
            actual.Params.Should().BeEmpty();
        }

        [Fact]
        public void ToStringSerializedAsJson()
        {
            var model = new Faker<ErrorResponseModel>()
                .RuleFor(r => r.Message, f => f.Lorem.Word())
                .Generate();

            model.ToString().Should().BeEquivalentTo(JsonSerializer.Serialize(model));
        }

        [Fact]
        public void SingleErrorAdditionCreatesSingleElementErrorArray()
        {
            var actual = new ErrorResponseModel();
            actual.AddError("error");

            actual.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void SingleErrorPairAdditionCreatesSingleElementErrorArray()
        {
            var actual = new ErrorResponseModel();
            var param = new KeyValuePair<string, string>("key", "value");
            actual.AddError(param.Key, param.Value);

            actual.Errors.Count.Should().Be(1);
        }
    }
}