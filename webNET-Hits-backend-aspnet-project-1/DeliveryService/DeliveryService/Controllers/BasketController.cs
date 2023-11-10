using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
using DeliveryService.Services;
using DeliveryService.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<DishBasketDTO>> Get()
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            var result = await _basketService.GetBasket(dbName, token);

            return result;
        }

        [HttpPost("dish/{dishId}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Post(Guid dishId)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");

            }
            await _basketService.AddDish(dishId, dbName, token);
            return Ok("dish succesfully added");
        }

        [HttpDelete("dish/{dishId}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(Guid dishId, bool increase = false)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");

            }
            await _basketService.DeleteDish(dishId, increase, dbName, token);

            if (increase)
            {
                return Ok("dish succesfully decreased");
            }
            return Ok("dish succesfully removed");

        }
    }
}
