using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Orders;
using MB.W2.GRLRestaurant.Entities;

namespace MB.W2.GRLRestaurant.WebApi.AutoMapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderListDto>();
            CreateMap<Order, OrderDetailsDto>();
            CreateMap<OrderDto, Order>().ReverseMap();

        }
    }
}
