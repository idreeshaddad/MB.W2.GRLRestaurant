using AutoMapper;
using MB.W2.GRLRestaurant.Dtos.Ingredients;
using MB.W2.GRLRestaurant.Entities;

namespace MB.W2.GRLRestaurant.WebApi.AutoMapperProfiles
{
    public class IngredientAutoMapperProfile : Profile
    {
        public IngredientAutoMapperProfile()
        {
            CreateMap<Ingredient, IngredientDto>().ReverseMap();
        }
    }
}
