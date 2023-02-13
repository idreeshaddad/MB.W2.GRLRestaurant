﻿namespace MB.W2.GRLRestaurant.Entities
{
    public class Order
    {
        public Order()
        {
            Meals = new List<Meal>();
        }

        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string Notes { get; set; }
        public bool IsPaid { get; set; }
        public DateTime OrderDate { get; set; }

        public List<Meal> Meals { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
