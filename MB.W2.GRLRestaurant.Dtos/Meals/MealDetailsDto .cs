using MB.W2.GRLRestaurant.Dtos.Ingredients;

namespace MB.W2.GRLRestaurant.Dtos.Meals
{
    public class MealDetailsDto
    {
        public MealDetailsDto()
        {
            Ingredients = new List<IngredientDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
