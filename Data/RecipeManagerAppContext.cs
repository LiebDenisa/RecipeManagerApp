using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Data
{
    public class RecipeManagerAppContext : DbContext
    {
        public RecipeManagerAppContext(DbContextOptions<RecipeManagerAppContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for each model
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
