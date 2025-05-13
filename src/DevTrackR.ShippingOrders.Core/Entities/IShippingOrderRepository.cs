namespace DevTrackR.ShippingOrders.Core.Entities
{
    public interface IShippingOrderRepository
    {
        Task AddAsync(ShippingOrder shippingOrder);
        Task<ShippingOrder> GetByCodeAsync(string code);
    }
}
