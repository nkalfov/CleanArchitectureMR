namespace CleanArchitecture.Application.Contracts
{
    public interface IInventoryService
    {
        void NotifySaleOccurred(int productId, int quantity);
    }
}