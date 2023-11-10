using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
using DeliveryService.Models.UserModels;
using DeliveryService.Services.Interfaces;
using DeliveryService.Services.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DeliveryService.Services
{
    public class BasketService: IBasketService
    {
        private readonly AppDbContext _context;
        private readonly TokenValidationMiddleware _tokenValidation;
        public BasketService(AppDbContext context, TokenValidationMiddleware tokenValidation)
        {
            _context = context;
            _tokenValidation = tokenValidation;
        }

        public async Task<IEnumerable<DishBasketDTO>> BasketListMapper(List<DishInBasket> baskets)
        {
            if (baskets == null)
            {
                throw new Exception("baskets is empty");
            }
            List<DishBasketDTO> dishesDTO = new List<DishBasketDTO>();
            for (int i = 0; i < baskets.Count; i++)
            {
                dishesDTO.Add(BasketMapper(baskets[i]).Result);
            }
            return dishesDTO;
        }
        public async Task<DishBasketDTO> BasketMapper(DishInBasket basket)
        {
            return new DishBasketDTO
            {
                Id = basket.Id,
                Name = basket.Name,
                Price = basket.Price,
                TotalPrice = basket.Price * basket.Amount,
                Image = basket.Image,
                Amount = basket.Amount
            };
        }
        private async Task AddDishInBasketToDB(User user, Dish dish)
        {
            await _context.DishInBaskets.AddAsync(new DishInBasket
            {
                Id = Guid.NewGuid(),
                UserId = user.NameId,
                DishId = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                Amount = 1,
                Image = dish.Image
            });
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<DishBasketDTO>> GetBasket(string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var dishesInBasket = await _context.DishInBaskets.Where(d => d.UserId == user.NameId && d.OrderId == null).ToListAsync();

            var result = await BasketListMapper(dishesInBasket);

            return result;
        }

        public async Task AddDish(Guid id, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var dish = await _context.Dishes.SingleOrDefaultAsync(h => h.Id == id);

            if (dish == null)
            {
                throw new NotFoundException("Cant find dish with that guid");
            }

            var dishInBasket = await _context.DishInBaskets.SingleOrDefaultAsync(d => d.UserId == user.NameId && d.OrderId == null && d.DishId == dish.Id);

            if (dishInBasket == null)
            {
                await AddDishInBasketToDB(user, dish);
            }
            else
            {
                dishInBasket.Amount++;
                await _context.SaveChangesAsync();
            }   
        }

        public async Task DeleteDish(Guid id, bool increase, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var dish = await _context.Dishes.SingleOrDefaultAsync(h => h.Id == id);

            if (dish == null)
            {
                throw new NotFoundException("Cant find dish with that guid");
            }

            var dishInBasket = await _context.DishInBaskets.SingleOrDefaultAsync(d => d.UserId == user.NameId && d.OrderId == null && d.DishId == dish.Id);

            if (dishInBasket == null)
            {
                throw new NotFoundException("Cant find this dish in your basket");
            }

            if (increase)
            {
                dishInBasket.Amount--;
                if (dishInBasket.Amount <= 0)
                    _context.DishInBaskets.Remove(dishInBasket);
            }
            else
            {
                _context.DishInBaskets.Remove(dishInBasket);
            }

            await _context.SaveChangesAsync();
        }
    }
}
