using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Data
{
    public class RecipeManagerAppContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public RecipeManagerAppContext(DbContextOptions<RecipeManagerAppContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } // Your existing DbSet
        public DbSet<Ingredient> Ingredients { get; set; } // Your existing DbSet
    }
}
