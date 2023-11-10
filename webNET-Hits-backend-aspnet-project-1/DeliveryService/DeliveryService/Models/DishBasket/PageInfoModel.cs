namespace DeliveryService.Models.DishBasket
{
    public class PageInfoModel
    {
        public int size { get; set; }
        public int count { get; set; }
        public int current { get; set; }

        public PageInfoModel() 
        {
            size = 5;
            count = 1;
            current = 1;
        }
        public PageInfoModel(int size, int count, int current = 1)
        {
            this.size = size;
            this.count = count;
            this.current = current;
        }
    }
}
