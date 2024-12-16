using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Data;
using RecipeManagerApp.Models;
using System.Linq;

namespace RecipeManagerApp.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly RecipeManagerAppContext _context;

        public IndexModel(RecipeManagerAppContext context)
        {
            _context = context;
        }

        public IList<Recipe> Recipes { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            var recipes = from r in _context.Recipes
                          select r;

            // Apply search filter
            if (!string.IsNullOrEmpty(SearchString))
            {
                recipes = recipes.Where(r => r.Title.Contains(SearchString) || r.Description.Contains(SearchString));
            }

            // Apply sorting
            switch (SortOrder)
            {
                case "name_desc":
                    recipes = recipes.OrderByDescending(r => r.Title);
                    break;
                case "time_asc":
                    recipes = recipes.OrderBy(r => r.PreparationTime);
                    break;
                case "time_desc":
                    recipes = recipes.OrderByDescending(r => r.PreparationTime);
                    break;
                default:
                    recipes = recipes.OrderBy(r => r.Title);
                    break;
            }

            Recipes = await recipes.ToListAsync();
        }
    }
}
