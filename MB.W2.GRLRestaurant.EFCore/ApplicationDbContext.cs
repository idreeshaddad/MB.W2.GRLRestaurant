using MB.W2.GRLRestaurant.Entities;
using Microsoft.EntityFrameworkCore;

namespace MB.W2.GRLRestaurant.EFCore
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}
