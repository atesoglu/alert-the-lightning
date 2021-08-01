using Domain.Models;
using Domain.Models.Base;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Models
{
    public class AssetModelTests
    {
        [Fact]
        public void ExtendsFromModelBase()
        {
            var actual = new AssetModel();
            actual.Should().BeAssignableTo<ModelBase>();
        }
    }
}