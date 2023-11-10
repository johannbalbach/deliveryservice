using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.DishBasket
{
    public class DishDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public double? Rating { get; set; }

        public string? Image { get; set; }

        public bool IsVegetarian { get; set; }
      
        public DishCategories Category { get; set; }
        
    }
}
