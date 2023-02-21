using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.W2.GRLRestaurant.Dtos.Orders
{
    public class OrderDto
    {
        public OrderDto()
        {
            MealIds = new List<int>();
        }

        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool IsPaid { get; set; } = false;


        public int CustomerId { get; set; }
        public List<int> MealIds { get; set; }
    }
}
