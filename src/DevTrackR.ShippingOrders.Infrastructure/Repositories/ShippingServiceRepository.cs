using DevTrackR.ShippingOrders.Core.Entities;
using MongoDB.Driver;

namespace DevTrackR.ShippingOrders.Infrastructure.Repositories
{
    public class ShippingServiceRepository : IShippingServiceRepository
    {
        private readonly IMongoCollection<ShippingService> _collection;

        public ShippingServiceRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<ShippingService>("shipping-services");
        }

        public async Task<List<ShippingService>> GetAllAsync() =>
             await _collection
                .Find(_ => true)
                .ToListAsync();
    }
}
