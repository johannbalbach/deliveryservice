using AutoMapper;
using DeliveryService.Models.DishBasket;
using DeliveryService.Models.Order;
using DeliveryService.Models.UserModels;

namespace DeliveryService.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderE, OrderDTO>();
            CreateMap<OrderE, OrderInfoDTO>()
                .ForMember(dib => dib.id, dto => dto.MapFrom(d => d.Id))
                .ForMember(dib => dib.deliveryTime, dto => dto.MapFrom(d => d.DeliveryTime))
                .ForMember(dib => dib.orderTime, dto => dto.MapFrom(d => d.OrderTime))
                .ForMember(dib => dib.status, dto => dto.MapFrom(d => (OrderStatus)d.StatusId))
                .ForMember(dib => dib.price, dto => dto.MapFrom(d => d.Price));
            CreateMap<DishInBasket, DishBasketDTO>()
                .ForMember(dib => dib.Id, dto => dto.MapFrom(d => d.DishId))
                .ForMember(dib => dib.Name, dto => dto.MapFrom(d => d.Name))
                .ForMember(dib => dib.Price, dto => dto.MapFrom(d => d.Price))
                .ForMember(dib => dib.TotalPrice, dto => dto.MapFrom(d => d.Price * d.Amount))
                .ForMember(dib => dib.Image, dto => dto.MapFrom(d => d.Image));
        }
    }
}
