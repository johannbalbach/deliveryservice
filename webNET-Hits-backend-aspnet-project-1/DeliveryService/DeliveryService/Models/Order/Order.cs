using DeliveryService.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Models.Order
{
    public class OrderE
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Guid Address { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
