using DeliveryService.Models.DishBasket;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.Order
{
    public class OrderDTO
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

        [Required]
        public List<DishBasketDTO> dishes { get; set; }

        [Required]
        [MinLength(1)]
        public Guid Address { get; set; }
    }
}
