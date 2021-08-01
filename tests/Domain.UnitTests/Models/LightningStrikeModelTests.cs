using Domain.Models;
using Domain.Models.Base;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models
{
    public class LightningStrikeModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new LightningStrikeModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }
    }
}