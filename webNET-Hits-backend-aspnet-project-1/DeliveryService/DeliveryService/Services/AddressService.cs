using DeliveryService.Models;
using DeliveryService.Models.AddressModles;
using DeliveryService.Models.Exceptions;
using DeliveryService.Models.UserModels;
using DeliveryService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Net;
using System.Text.RegularExpressions;

namespace DeliveryService.Services
{
    public class AddressService: IAddressService
    {
        private readonly AddressContext _context;
        public AddressService(AddressContext context)
        {
            _context = context;
        }

        private async Task<SearchAddressModel> SearchAddressMapper(Address address)
        {
            return new SearchAddressModel
            {
                Objectid = address.Id,
                ObjectGuid = address.Objectguid,
                text = address.Typename + " " + address.Name,
                objectLevel = (GarAddressLevel) int.Parse(address.Level),
                objectLevelText = address.Level
            };
        }
        private async Task<SearchAddressModel> SearchAddressMapper(House house)
        {
            var result = new SearchAddressModel
            {
                Objectid = house.Id,
                ObjectGuid = house.Objectguid,
                text = house.Housenum,
                objectLevel = GarAddressLevel.Building,
                objectLevelText = "10"
            };
            if (house.Addnum1 != null)
                result.text = result.text + " стр. " + house.Addnum1;
            if (house.Addnum2 != null)
                result.text = result.text + " соор." + house.Addnum2;
            return result;
        }
        public async Task<ActionResult<IEnumerable<SearchAddressModel>>> search(long parentObjectId, string query)
        {
/*            var heirarchieList = await _context.AsAdmHierarchies.Where(a => a.Parentobjid == parentObjectId).ToListAsync();

            List<SearchAddressModel> result = new List<SearchAddressModel>();

            string pattern = $"{query}";

            foreach (Hierarchy h in heirarchieList)
            {
                var address = await _context.AsAddrObjs.FirstOrDefaultAsync(a => a.Objectid == h.Objectid);
                var house = await _context.AsHouses.FirstOrDefaultAsync(a => a.Objectid == h.Objectid);

                if (address != null)
                {
                    if (Regex.IsMatch(address.Name, pattern, RegexOptions.IgnoreCase))
                        result.Add(SearchAddressMapper(address).Result);
                }

                if (house != null)
                {
                    if (Regex.IsMatch(house.Housenum, pattern, RegexOptions.IgnoreCase))
                        result.Add(SearchAddressMapper(house).Result);
                }

            }


            var result = await _context.AsAdmHierarchies.Where(a => a.Parentobjid == parentObjectId)
                        return result;
*/


            var addr = await _context.AsAddrObjs
                                .Join(
                                    _context.AsAdmHierarchies.Where(a => a.Parentobjid == parentObjectId),
                                    addr => addr.Objectid,
                                    hierarchy => hierarchy.Objectid,
                                    (addr, hierarchy) => new SearchAddressModel
                                    {
                                        Objectid = addr.Objectid,
                                        ObjectGuid = addr.Objectguid,
                                        text = addr.Typename + " " + addr.Name,
                                        objectLevel = (GarAddressLevel)int.Parse(addr.Level),
                                        objectLevelText = addr.Level,
                                    })
                                .Where(model => EF.Functions.ILike(model.text, "%" + query + "%")).ToListAsync();

            var house = await _context.AsAdmHierarchies.Where(a => a.Parentobjid == parentObjectId)
                               .Join(_context.AsHouses,
                                   hierarchy => hierarchy.Objectid,
                                   house => house.Objectid,
                                   (hierarchy, house) =>
                                       new SearchAddressModel
                                       {
                                           Objectid = house.Objectid,
                                           ObjectGuid = house.Objectguid,
                                           text = house.Addnum2 != null ? house.Addnum1 != null ? house.Housenum + " стр. " + house.Addnum1 + " соор." + house.Addnum2 : house.Housenum + " стр. " + house.Addnum1 : house.Housenum,
                                           objectLevel = GarAddressLevel.Building,
                                           objectLevelText = "10",
                                       })
                               .Where(model => EF.Functions.ILike(model.text, "%" + query + "%")).ToListAsync();
            addr.AddRange(house);
            return addr;
        }
        public async Task<ActionResult<IEnumerable<SearchAddressModel>>> chain(Guid ObjectGuid)
        {
            var addressQuery = _context.AsAddrObjs.OrderByDescending(a => a.Enddate);
            var houseQuery = _context.AsHouses.OrderByDescending(a => a.Enddate);

            var address = await addressQuery.FirstOrDefaultAsync(a => a.Objectguid == ObjectGuid);
            var house = await houseQuery.FirstOrDefaultAsync(a => a.Objectguid == ObjectGuid);

            if (address == null && house == null)
            {
                throw new NotFoundException("we cant find address with that guid");
            }
            List<SearchAddressModel> result = new List<SearchAddressModel>();

            if (address == null)
                return await findChain(house, result);
            if (house == null)
                return await findChain(address, result);

            throw new Exception("unpredictable exception");
        }

        private async Task<ActionResult<IEnumerable<SearchAddressModel>>> findChain(House house, List<SearchAddressModel> result)
        {
            result.Add(SearchAddressMapper(house).Result);

            var hierarchy = await _context.AsAdmHierarchies.FirstOrDefaultAsync(h => h.Objectid == house.Objectid);

            if (hierarchy == null)
            {
                return result;
            }
            var temp = await _context.AsHouses.FirstOrDefaultAsync(a => a.Objectid == hierarchy.Parentobjid);

            while (hierarchy.Parentobjid != 0 && temp != null)
            {
                result.Add(SearchAddressMapper(temp).Result);

                hierarchy = await _context.AsAdmHierarchies.FirstOrDefaultAsync(h => h.Objectid == temp.Objectid);

                if (hierarchy == null)
                {
                    result.Reverse();
                    return result;
                }

                temp = await _context.AsHouses.FirstOrDefaultAsync(a => a.Objectid == hierarchy.Parentobjid);
            }

            var address = await _context.AsAddrObjs.FirstOrDefaultAsync(a => a.Objectid == hierarchy.Parentobjid);

            if (address == null)
            {
                result.Reverse();
                return result;
            }

            return await findChain(address, result);
        }

        private async Task<ActionResult<IEnumerable<SearchAddressModel>>> findChain(Address address, List<SearchAddressModel> result)
        {
            result.Add(SearchAddressMapper(address).Result);

            var hierarchy = await _context.AsAdmHierarchies.FirstOrDefaultAsync(h => h.Objectid == address.Objectid);

            if (hierarchy == null)
            {
                result.Reverse();
                return result;
            }

            while (hierarchy.Parentobjid != 0)
            {
                var temp = await _context.AsAddrObjs.FirstOrDefaultAsync(a => a.Objectid == hierarchy.Parentobjid);

                result.Add(SearchAddressMapper(temp).Result);

                hierarchy = await _context.AsAdmHierarchies.FirstOrDefaultAsync(h => h.Objectid == temp.Objectid);

                if (hierarchy == null)
                {
                    result.Reverse();
                    return result;
                }
            }
            result.Reverse();

            return result;
        }
    }
}
