using AutoMapper;
using WebApiAdvanceExample.Entities;
using WebApiAdvanceExample.Entities.DTOs.OrderDTOs;

namespace WebApiAdvanceExample.Profiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles()
        {
            CreateMap<Order, GetOrderDto>();

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.CreatedAt,opt =>
                    opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt,opt =>
                    opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.OrderDate,opt =>
                    opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateOrderDto, Order>()
                .ForMember(dest => dest.UpdatedAt, opt =>
                    opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.OrderDate, opt=>
                opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
