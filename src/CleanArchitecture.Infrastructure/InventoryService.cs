
using System;
using CleanArchitecture.Application.Contracts;
using CleanArchitecture.Common.Options;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure
{
	public class InventoryService : IInventoryService
	{
        private readonly Inventory _inventory;

		public InventoryService(
            IOptionsSnapshot<Inventory> inventorySnapshot)
		{
            _inventory = inventorySnapshot.Value;
		}

        public void NotifySaleOccurred(int productId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
