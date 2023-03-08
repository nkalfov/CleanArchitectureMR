
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
        private readonly InventoryOptions _inventory;
        private readonly HttpClient _httpClient;

        public InventoryService(
            IOptionsSnapshot<InventoryOptions> inventorySnapshot,
            HttpClient httpClient)
		{
            _httpClient = httpClient;
            _inventory = inventorySnapshot.Value;
		}

        public async Task NotifySaleOccurredAsync(long productId, int quantity)
        {
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

            using var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
