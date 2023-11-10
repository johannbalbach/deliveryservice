using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.Order
{
    public class OrderInfoDTO
    {
        public Guid id { get; set; }

        [Required]
        public DateTime deliveryTime { get; set; }
        [Required]
        public DateTime orderTime { get; set; }
        [Required]
        public OrderStatus status { get; set; }
        [Required]
        public double price { get; set; }
    }
}
