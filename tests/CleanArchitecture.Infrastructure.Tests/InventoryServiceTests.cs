using System;
using System.Net;
using CleanArchitecture.Common.Options;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;

namespace CleanArchitecture.Infrastructure.Tests
{
    public class InventoryServiceTests
    {
        private readonly Mock<IOptionsSnapshot<Inventory>> _inventorySnapshotMock;

        public InventoryServiceTests()
        {
            var inventory = new Inventory
            {
                UrlBase = "https://domain.com",
                PathNotifySale = "/notify/{0}"
            };

            _inventorySnapshotMock = new Mock<IOptionsSnapshot<Inventory>>();
            _inventorySnapshotMock
                .Setup(x => x.Value)
                .Returns(inventory);
        }

        [Theory(DisplayName = "Successful Notification")]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NoContent)]
        public void SuccessfulNotification(HttpStatusCode statusCode)
        {
            var delegatingHandlerMock = CreatDelegatigHandlerMock(statusCode);

            var httpClient = new HttpClient(delegatingHandlerMock);

            var inventoryService = new InventoryService(
                _inventorySnapshotMock.Object,
                httpClient);

            inventoryService
                .NotifySaleOccurredAsync(1, 1)
                .GetAwaiter()
                .GetResult();
        }

        [Theory(DisplayName = "Notification Throws HttpRequestException")]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadGateway)]
        public void UnsuccessfulNotification(HttpStatusCode statusCode)
        {
            var delegatingHandler = CreatDelegatigHandlerMock(statusCode);

            var httpClient = new HttpClient(delegatingHandler);

            var inventoryService = new InventoryService(
                _inventorySnapshotMock.Object,
                httpClient);

            Assert.Throws<HttpRequestException>(() =>
            {
                inventoryService
                    .NotifySaleOccurredAsync(1, 1)
                    .GetAwaiter()
                    .GetResult();
            });
        }

        private static DelegatingHandler CreatDelegatigHandlerMock(HttpStatusCode httpStatus)
        {
            var delegatingHandlerMock = new Mock<DelegatingHandler>();
            delegatingHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(httpStatus))
                .Verifiable();

            delegatingHandlerMock
                .As<IDisposable>()
                .Setup(x => x.Dispose());

            return delegatingHandlerMock.Object;
        }
    }
}

