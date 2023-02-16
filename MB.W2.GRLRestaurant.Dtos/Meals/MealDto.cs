namespace MB.W2.GRLRestaurant.Dtos.Meals
{
    public class MealDto
    {
        public MealDto()
        {
            IngredientIds = new List<int>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<int> IngredientIds { get; set; }
    }
}
