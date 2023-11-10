using DeliveryService.Models;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Exceptions;
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
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<DishPagedListDTO>> GetListOfDishes([FromQuery]GetListOfDishesQuery query)
        {
            var result = await _dishService.GetDishList(query);

            return result;
        }

        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DishDTO>> GetDish(Guid id)
        {
            var result = await _dishService.GetDish(id);

            return result;
        }

        [HttpGet("{id}/rating/check")]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<bool> Check(Guid id)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            var result = await _dishService.CheckUserPermission(id, dbName, token);

            return result;
        }

        [HttpPost("{id}/rating")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
        [SwaggerResponse((int)HttpStatusCode.Forbidden)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        [Authorize]
        public async Task<ActionResult> SetRating(Guid id, int ratingScore)
        {
            var dbName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
            {
                throw new InvalidTokenException("Token not found");
            }

            await _dishService.SetRating(id, dbName, token, ratingScore);

            return Ok();
        }
    }
}
