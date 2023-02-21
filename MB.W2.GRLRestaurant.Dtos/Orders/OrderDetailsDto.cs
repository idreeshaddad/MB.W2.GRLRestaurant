using MB.W2.GRLRestaurant.Dtos.Meals;

namespace MB.W2.GRLRestaurant.Dtos.Orders
{
    public class OrderDetailsDto
    {
        public OrderDetailsDto()
        {
            Meals = new List<MealListDto>();
        }

        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerFullName { get; set; }
        public double TotalPrice { get; set; }
        public List<MealListDto> Meals { get; set; }
    }
}
