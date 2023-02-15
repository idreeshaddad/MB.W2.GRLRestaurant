using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Customers;
using MB.W2.GRLRestaurant.Entities;

namespace MB.W2.GRLRestaurant.WebApi.AutoMapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerDetailsDto>();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
