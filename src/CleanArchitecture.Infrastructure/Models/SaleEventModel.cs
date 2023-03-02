using System;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Infrastructure.Models
{
	public class SaleEventModel
	{
		public SaleEventModel()
		{
			// do nothing
		}

		public SaleEventModel(long productId, int quantity)
		{
			ProductId = productId;
			Quantity = quantity;
		}

		[JsonPropertyName("productId")]
		public long ProductId { get; set; }

		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }
	}
}

