using DeliveryService.Models.DishBasket;
using DeliveryService.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Models.Order;

namespace DeliveryService.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> BanedTokens { get; set; }

        public DbSet<Dish> Dishes { get; set; }

        public DbSet<OrderE> Orders { get; set; }

        public DbSet<DishInBasket> DishInBaskets { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
