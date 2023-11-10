using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.Order
{
    public class OrderCreateDTO
    {
        [Required]
        public DateTime deliveryTime { get; set; }

        [Required]
        public Guid Address { get; set; }
    }
}
