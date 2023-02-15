using MB.W2.GRLRestaurant.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MB.W2.GRLRestaurant.Entities
{
    public class Customer
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public List<Order> Orders { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [NotMapped]
        public int Age { 
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }
    }
}
