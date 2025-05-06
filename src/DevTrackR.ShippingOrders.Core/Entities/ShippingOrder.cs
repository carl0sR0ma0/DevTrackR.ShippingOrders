using DevTrackR.ShippingOrders.Core.Enums;
using DevTrackR.ShippingOrders.Core.ValueObjects;

namespace DevTrackR.ShippingOrders.Core.Entities
{
    public class ShippingOrder : EntityBase
    {
        public ShippingOrder(string description, decimal weightInKg, DeliveryAddress deliveryAddress)
        {
            TrackingCode = GenerateTrackingCode();
            Description = description;
            PostedAt = DateTime.Now;
            WeightInKg = weightInKg;
            DeliveryAddress = deliveryAddress;
            Status = ShippingOrderStatus.Started;
            Services = new List<ShippingOrderService>();
        }

        public string TrackingCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; }
        public decimal WeightInKg { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public ShippingOrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ShippingOrderService> Services { get; set; }

        public void SetupServices(List<ShippingService> services)
        {
            foreach (var service in services)
            {
                var serviceService = service.FixedPrice * service.PricePerKg * WeightInKg;
                TotalPrice += serviceService;
                Services.Add(new ShippingOrderService(service.Title, serviceService));
            }
        }

        private static string GenerateTrackingCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            var code = new char[10];
            var random = new Random();

            for (var i = 0; i < 5; i++) {
                code[i] = chars[random.Next(chars.Length)];
            }

            for (var i = 5; i < 10; i++) {
                code[i] = numbers[random.Next(numbers.Length)];
            }

            return new String(code);
        }
    }
}