using DeliveryService.Models.DishBasket;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<DishBasketDTO>> GetBasket(string dbName, string token);

        Task AddDish(Guid id, string dbName, string token);

        Task DeleteDish(Guid id, bool increase, string dbName, string token);
    }
}
