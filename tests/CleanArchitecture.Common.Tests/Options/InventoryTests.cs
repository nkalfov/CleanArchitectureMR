using System;
using CleanArchitecture.Common.Options;
using Xunit;

namespace CleanArchitecture.Common.Tests.Options
{
    public class InventoryTests
    {
        private readonly Inventory _inventory;

        private const string _baseUrl = "https://domain.com";
        private const string _pathNotifySale = "/product/{0}/sale/notify";

        public InventoryTests()
        {
            _inventory = new Inventory
            {
                UrlBase = _baseUrl,
                PathNotifySale = _pathNotifySale
            };
        }

        [Fact(DisplayName = "BaseUrl Returns a Domain to a service")]
        public void UrlBaseReturnsDomain()
        {
            // Act
            var actual = _inventory.UrlBase;

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(_baseUrl, actual);
        }

        [Fact(DisplayName = "PathNotifySale returns the path to an endpoint")]
        public void PathNotifySaleReturnsPathToAnEndpoint()
        {
            // Act
            var actual = _inventory.PathNotifySale;

            // Assert
            Assert.NotEmpty(actual);
            Assert.Equal(_pathNotifySale, actual);
        }

        [Theory(DisplayName = "GetUrlNotifySale Returns a URI to a Remote Action")]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(123)]
        public void GetUrlNotifySaleReturnsAUriToARemoteAction(long productId)
        {
            // Arrrange
            var expectedString = string.Concat(
                _baseUrl,
                string.Format(_pathNotifySale, productId));

            var expected = new Uri(expectedString);
            
            // Act
            var actual = _inventory.GetUrlNotifySale(productId);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}

