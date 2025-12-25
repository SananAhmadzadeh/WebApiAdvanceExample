using AutoMapper;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.OrderItemDTOs;

namespace WebApiAdvanceExample.Profiles
{
    public class OrderItemProfiles : Profile
    {
        public OrderItemProfiles()
        {
            CreateMap<OrderItem, GetOrderItemDto>();
            CreateMap<CreateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.CreatedAt, opt =>
                    opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt =>
                    opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateOrderItemDto, OrderItem>()
                .ForMember(dest => dest.UpdatedAt, opt =>
                    opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
