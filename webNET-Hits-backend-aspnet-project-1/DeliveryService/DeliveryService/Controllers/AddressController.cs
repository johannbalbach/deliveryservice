using DeliveryService.Models;
using DeliveryService.Models.AddressModles;
using DeliveryService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Numerics;


namespace DeliveryService.Controllers
{

    [Route("api/address/[action]")]
    [ApiController]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(Response), Description = "InternalServerError")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _context;

        public AddressController(IAddressService context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchAddressModel>>> search(long parentObjectId, string? query)
        {
            return await _context.search(parentObjectId, query);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchAddressModel>>> chain(Guid ObjectGuid)
        {
            return await _context.chain(ObjectGuid);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchAddressModel>>> getaddresschain(Guid ObjectGuid) 
        {
            return await _context.chain(ObjectGuid);
        }
    }
}
