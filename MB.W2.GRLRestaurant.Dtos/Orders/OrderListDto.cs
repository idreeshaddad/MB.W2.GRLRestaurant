namespace MB.W2.GRLRestaurant.Dtos.Orders
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool IsPaid { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
    }
}
