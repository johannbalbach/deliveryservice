using DeliveryService.Models.UserModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.DishBasket
{
    public class Rating
    {
        public Guid Id { get; set; }
        [Required]
        public Guid DishId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int RatingScore { get; set; }
    }
}
