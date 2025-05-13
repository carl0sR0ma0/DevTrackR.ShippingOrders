namespace DevTrackR.ShippingOrders.Core.Entities
{
    public interface IShippingServiceRepository
    {
        Task<List<ShippingService>> GetAllAsync();
    }
}
