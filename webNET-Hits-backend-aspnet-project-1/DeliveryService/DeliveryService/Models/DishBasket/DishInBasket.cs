using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Models.DishBasket
{
    public class DishInBasket
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Guid))]
        public Guid UserId { get; set; }
        public Guid DishId { get; set; }

        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Amount { get; set; }

        public string? Image { get; set; }
        public Guid? OrderId { get; set; }//?
    }
}
