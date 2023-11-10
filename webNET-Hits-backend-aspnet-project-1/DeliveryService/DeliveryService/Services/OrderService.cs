using AutoMapper;
using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
using DeliveryService.Models.Order;
using DeliveryService.Models.UserModels;
using DeliveryService.Services.Interfaces;
using DeliveryService.Services.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services
{
    public class OrderService: IOrderService
    {
        private readonly AppDbContext _context;
        private readonly TokenValidationMiddleware _tokenValidation;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, TokenValidationMiddleware tokenValidation, IMapper mapper)
        {
            _context = context;
            _tokenValidation = tokenValidation;
            _mapper = mapper;
        }

        private async Task<OrderDTO> OrderMapper(OrderE order)
        {
            return new OrderDTO
            {
                id = order.Id,
                deliveryTime = order.DeliveryTime,
                orderTime = order.OrderTime,
                status = (OrderStatus)order.StatusId,
                price = order.Price,
                Address = order.Address,
                dishes = _mapper.Map<List<DishInBasket>, List<DishBasketDTO>>(_context.DishInBaskets.Where(d => d.UserId == order.UserId && d.OrderId == order.Id).ToListAsync().Result)//MAPPER
            };
        }
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var temp = await _context.Orders.SingleOrDefaultAsync(h => h.Id == id);

            if (temp == null)
            {
                throw new NotFoundException("Cant find order with that guid");
            }

            var orderDTO = OrderMapper(temp);

/*            var dishesInBasket = await _context.DishInBaskets.Where(d => d.UserId == user.NameId && d.OrderId == id).ToListAsync();
            orderDTO.Result.dishes = _mapper.Map<List<DishInBasket>, List<DishBasketDTO>>(dishesInBasket);*/

            return orderDTO.Result;
        }
        public async Task<ActionResult<List<OrderInfoDTO>>> GetListOfOrders(string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var orders = await _context.Orders.Where(d => d.UserId == user.NameId).ToListAsync();

            var result = _mapper.Map<List<OrderE>, List<OrderInfoDTO>>(orders);

            return result ;
        }
        public async Task CreateOrder(OrderCreateDTO order, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var dishesInBasket = await _context.DishInBaskets.Where(d => d.UserId == user.NameId && d.OrderId == null).ToListAsync();

            if (dishesInBasket.Count == 0)
            {
                throw new BadRequestException("your basket is empty");
            }

            var dishBasketDTO = _mapper.Map<List<DishInBasket>, List<DishBasketDTO>>(dishesInBasket);

            double price = 0;

            foreach (DishBasketDTO dish in dishBasketDTO)
            {
                price += dish.TotalPrice;
            }


            OrderE temp = new OrderE
            {
                Id = Guid.NewGuid(),
                DeliveryTime = order.deliveryTime,
                OrderTime = DateTime.UtcNow,
                StatusId = (int)OrderStatus.InProcess,
                Price = price,
                Address = order.Address == null ? user.AddressId != null ? (Guid)user.AddressId : order.Address : order.Address,
                UserId = user.NameId
            };
            await _context.Orders.AddAsync(temp);
            await _context.SaveChangesAsync();

            foreach (DishInBasket dish in dishesInBasket)
            {
                dish.OrderId = temp.Id;
            }

            await _context.SaveChangesAsync();
        }
        public async Task ConfirmDelivery(Guid id, string dbName, string token)
        {
            await _tokenValidation.IsTokenValid(token);

            var user = await _context.Users.SingleOrDefaultAsync(h => h.dbName == dbName);

            if (user == null)
            {
                throw new InvalidLoginException();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(d => d.Id == id);

            if (order == null)
            {
                throw new NotFoundException("we cant find order with that guid");
            }
            order.StatusId = (int) OrderStatus.Delivered;

            await _context.SaveChangesAsync();
        }
    }
}
