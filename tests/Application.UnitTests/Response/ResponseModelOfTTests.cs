using System.Text.Json;
using Application.Response;
using Bogus;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Response
{
    public class ResponseModelOfTTests
    {
        [Fact]
        public void DefaultConstructorCreatesEmptyData()
        {
            var actual = new ResponseModel<string>();
            actual.Data.Should().BeNullOrEmpty();
        }

        [Fact]
        public void DefaultConstructorCreatesTotalOfOne()
        {
            var actual = new ResponseModel<string>();
            actual.Total.Should().Be(0);
        }

        [Fact]
        public void GenericTypeOfTShouldOfString()
        {
            var actual = new ResponseModel<string>();
            actual.Data.Should().BeNullOrEmpty();
        }

        [Fact]
        public void GenericTypeOfTShouldOfObject()
        {
            var actual = new ResponseModel<object>();
            actual.Data.Should().BeNull();
        }

        [Fact]
        public void GenericTypeOfTShouldOfBoolean()
        {
            var actual = new ResponseModel<bool>();
            actual.Data.Should().Be(false);
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