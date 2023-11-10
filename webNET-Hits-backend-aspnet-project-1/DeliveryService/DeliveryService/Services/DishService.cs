using AutoMapper;
using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
using DeliveryService.Models.UserModels;
using DeliveryService.Services.Interfaces;
using DeliveryService.Services.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DeliveryService.Services
{
    public class DishService: IDishService
    {
        private readonly AppDbContext _context;
        private readonly TokenValidationMiddleware _tokenValidation;

        public DishService(AppDbContext context, TokenValidationMiddleware tokenValidation)
        {
            _context = context;
            _tokenValidation = tokenValidation;
        }

        private async Task<List<DishDTO>> DishListMapper(List<Dish> dishes)
        {
            List<DishDTO> dishesDTO = new List<DishDTO>();
            for (int i = 0; i< dishes.Count; i++)
            {
                dishesDTO.Add(DishMapper(dishes[i]).Result);
            }
            return dishesDTO;
        }
        private async Task<DishDTO> DishMapper(Dish dish)
        {
            return new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Rating = dish.Rating,
                Image = dish.Image,
                IsVegetarian = dish.IsVegetarian,
                Category = (DishCategories)dish.CategoryId
            };
        }

        public async Task<ActionResult<DishPagedListDTO>> GetDishList(GetListOfDishesQuery query)
        {
            var totalDishesCount = await _context.Dishes.CountAsync();

            if (totalDishesCount == 0) 
            {
                throw new Exception("totalDishesCount was 0");
            }

            int pageSize = 5;//??
            int page = query.Page;

            var dishesQuery = _context.Dishes.Where(d => query.Categories.Contains((DishCategories)d.CategoryId));

            if (query.Vegetarian)
            {
                dishesQuery = dishesQuery.Where(d => d.IsVegetarian);
            }

            var dishes = query.Sorting switch
            {
                DishSorting.NameAsc => await dishesQuery.OrderBy(d => d.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                DishSorting.NameDesc => await dishesQuery.OrderByDescending(d => d.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                DishSorting.PriceAsc => await dishesQuery.OrderBy(d => d.Price).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                DishSorting.PriceDesc => await dishesQuery.OrderByDescending(d => d.Price).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                DishSorting.RatingAsc => await dishesQuery.OrderBy(d => d.Rating.HasValue).ThenBy(d => d.Rating).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                DishSorting.RatingDesc => await dishesQuery.OrderByDescending(d => d.Rating.HasValue).ThenByDescending(d => d.Rating).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
                _ => await dishesQuery.OrderBy(d => d.Name).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(),
            };

            if(dishes.Count == 0)
            {
                throw new BadRequestException("cant take dishes");
            }

            var dishesDTO = DishListMapper(dishes);
            return new DishPagedListDTO
            {
                dishes = dishesDTO.Result,
                pagination = new PageInfoModel(pageSize, 1, page)
            };
        }

        public async Task<ActionResult<DishDTO>> GetDish(Guid Id)
        {
            var temp = await _context.Dishes.SingleOrDefaultAsync(h => h.Id == Id);

            if (temp == null)
            {
                throw new NotFoundException("Cant find dish with that guid");
            }

            var dishDTO = DishMapper(temp);

            return dishDTO.Result;
        }

        public async Task<bool> CheckUserPermission(Guid id, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);
            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var temp = await _context.Dishes.SingleOrDefaultAsync(h => h.Id == id);

            if (temp == null)
            {
                throw new NotFoundException("Cant find dish with that guid");
            }

            var carts = await _context.DishInBaskets.FirstOrDefaultAsync(x => x.DishId == id && x.UserId == user.NameId && x.OrderId != null);

            return carts != null;
        }

        public async Task SetRating(Guid id, string dbName, string token, int ratingScore)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(h => h.Id == id);

            if (dish == null)
            {
                throw new NotFoundException("Cant find dish with that guid");
            }

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            if (!CheckUserPermission(id, dbName, token).Result)
            {
                throw new InvalidTokenException("you cant set rating to this dish");
            }

            await _context.Ratings.AddAsync(new Rating
            {
                Id = Guid.NewGuid(),
                DishId = id,
                UserId = user.NameId,
                RatingScore = ratingScore
            });
            await _context.SaveChangesAsync();

            var dishRatingList = await _context.Ratings.Where(x => x.DishId == id).ToListAsync();
            var sum = dishRatingList.Sum(r => r.RatingScore);
            dish!.Rating = (double)sum / dishRatingList.Count;

            await _context.SaveChangesAsync();
        }
    }
}
