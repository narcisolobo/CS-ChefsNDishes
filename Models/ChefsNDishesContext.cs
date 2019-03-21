using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Models {
    public class ChefsNDishesContext : DbContext {
        public ChefsNDishesContext(DbContextOptions<ChefsNDishesContext> options) : base(options) { }
        public DbSet<Chef> Chefs {get;set;}
        public DbSet<Dish> Dishes {get;set;}
    }
}