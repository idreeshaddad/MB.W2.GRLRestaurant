using MB.W2.GRLRestaurant.Utils.Enums;

namespace MB.W2.GRLRestaurant.Dtos.Customers
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        //public List<OrderDto> Orders { get; set; }
    }
}
