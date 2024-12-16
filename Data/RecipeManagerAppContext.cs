using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Data
{
    public class RecipeManagerAppContext : IdentityDbContext
    {
        public RecipeManagerAppContext(DbContextOptions<RecipeManagerAppContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
