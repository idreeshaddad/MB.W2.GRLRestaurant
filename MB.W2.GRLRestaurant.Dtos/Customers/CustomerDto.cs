using MB.W2.GRLRestaurant.Utils.Enums;

namespace MB.W2.GRLRestaurant.Dtos.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
