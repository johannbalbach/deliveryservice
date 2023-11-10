using DeliveryService.Models.AddressModles;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.Interfaces
{
    public interface IAddressService
    {
        Task<ActionResult<IEnumerable<SearchAddressModel>>> search(long parentObjectId, string query);
        Task<ActionResult<IEnumerable<SearchAddressModel>>> chain(Guid ObjectGuid);
    }
}
