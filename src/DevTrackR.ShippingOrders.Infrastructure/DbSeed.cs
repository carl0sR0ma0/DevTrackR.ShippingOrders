﻿using DevTrackR.ShippingOrders.Core.Entities;
using MongoDB.Driver;

namespace DevTrackR.ShippingOrders.Infrastructure
{
    public class DbSeed
    {
        private readonly IMongoCollection<ShippingService> _collection;
        private readonly List<ShippingService> _shippingService = new()
        {
            new("Envio estatudal", 3.75m, 12),
            new("Envio internacional", 5.25m, 15),
            new("Caixa tamanho P", 0, 5),
        };

        public DbSeed(IMongoDatabase database)
        {
            _collection = database.GetCollection<ShippingService>("shipping-services");
        }

        public void Populate()
        {
            if (_collection.CountDocuments(c=> true) == 0) _collection.InsertMany(_shippingService);
        }
    }
}