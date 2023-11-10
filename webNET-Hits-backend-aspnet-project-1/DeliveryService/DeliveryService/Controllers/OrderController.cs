using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
using DeliveryService.Models.Order;
using DeliveryService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace DeliveryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response), Description = "InternalServerError")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            var result = await _orderService.GetOrder(id, dbName, token);

            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderInfoDTO>>> GetListOfDishes()
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            var result = await _orderService.GetListOfOrders(dbName, token);

            return result;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDTO order)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            await _orderService.CreateOrder(order, dbName, token);

            return Ok();
        }

        [HttpPost("{id}/status")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> ConfirmDelivery(Guid id)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            await _orderService.ConfirmDelivery(id, dbName, token);

            return Ok();
        }
    }
}
