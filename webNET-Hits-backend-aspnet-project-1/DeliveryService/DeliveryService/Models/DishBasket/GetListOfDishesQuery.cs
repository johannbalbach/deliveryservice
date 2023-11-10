using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models.DishBasket
{
    public class GetListOfDishesQuery
    {
        public List<DishCategories> Categories { get; set; } = new List<DishCategories>();
        public bool Vegetarian { get; set; }

        [EnumDataType(typeof(DishSorting))] 
        public DishSorting Sorting { get; set; }
        public int Page { get; set; } = 1;
    }
}
