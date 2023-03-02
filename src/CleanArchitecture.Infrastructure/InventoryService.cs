
using System;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Common.Options;
using CleanArchitecture.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure
{
	public class InventoryService : IInventoryService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Inventory _inventory;

        public InventoryService(
            IHttpClientFactory httpClientFactory,
            IOptionsSnapshot<Inventory> inventorySnapshot)
		{
            _httpClientFactory = httpClientFactory;
            _inventory = inventorySnapshot.Value;
		}

        public async Task NotifySaleOccurredAsync(long productId, int quantity)
        {
            var client = _httpClientFactory
                .CreateClient(nameof(InventoryService));

            var requestUri = _inventory.GetUrlNotifySale(productId);
            var requestObject = new SaleEventModel(productId, quantity);
            var requestJson = JsonSerializer.Serialize(requestObject);

            using var requestContent = new StringContent(
                requestJson,
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var request = new HttpRequestMessage(
                HttpMethod.Post,
                requestUri);

            using var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
