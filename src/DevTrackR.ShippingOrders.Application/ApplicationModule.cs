﻿using DevTrackR.ShippingOrders.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DevTrackR.ShippingOrders.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            return services;
        }

        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IShippingOrderService, ShippingOrderService>();
            services.AddScoped<IShippingServiceService, ShippingServiceService>();
            return services;
        }
    }
}
