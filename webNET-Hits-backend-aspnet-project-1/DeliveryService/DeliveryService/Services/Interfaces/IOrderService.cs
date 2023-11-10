using DeliveryService.Models.Order;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.Interfaces
{
    public interface IOrderService
    {
         Task <ActionResult<OrderDTO>> GetOrder(Guid id, string dbName, string token);
         Task<ActionResult<List<OrderInfoDTO>>> GetListOfOrders(string dbName, string token);
         Task CreateOrder(OrderCreateDTO order, string dbName, string token);
         Task ConfirmDelivery(Guid id, string dbName, string token);
    }
}
