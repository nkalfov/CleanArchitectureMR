namespace CleanArchitecture.Application.Contracts
{
    public interface IInventoryService
    {
        Task NotifySaleOccurredAsync(long productId, int quantity);
    }
}