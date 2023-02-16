using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Meals;
using MB.W2.GRLRestaurant.Entities;

namespace MB.W2.GRLRestaurant.WebApi.AutoMapperProfiles
{
    public class MealAutoMapperProfile : Profile
    {
        public MealAutoMapperProfile()
        {
            CreateMap<Meal, MealListDto>();
            CreateMap<Meal, MealDetailsDto>();
            CreateMap<Meal, MealDto>().ReverseMap();
        }
    }
}
