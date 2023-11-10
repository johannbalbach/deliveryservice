namespace DeliveryService.Models.DishBasket
{
    public class DishPagedListDTO
    {
        public List<DishDTO>? dishes { get; set; }
        public PageInfoModel pagination { get; set; }
    }
}
