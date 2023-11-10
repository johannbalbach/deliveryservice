using DeliveryService.Models.DishBasket;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.Interfaces
{
    public interface IDishService
    {
        Task<ActionResult<DishPagedListDTO>> GetDishList(GetListOfDishesQuery query);

        Task<ActionResult<DishDTO>> GetDish(Guid id);

        Task<bool> CheckUserPermission(Guid id, string dbName, string token);

        Task SetRating(Guid id, string dbName, string token, int ratingScore);
    }
}
