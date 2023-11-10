using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.DishBasket
{
    public class DishBasketDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        [Required]
        public int Amount { get; set; }

        public string? Image { get; set; }

/*        public DishBasketDTO(string name, double price, int amount, string? image, Guid id)
        {
            Name = name;
            Price = price;
            TotalPrice = price*amount;
            this.amount = amount;
            this.image = image;
            this.id = id;
        }*/
    }
}
