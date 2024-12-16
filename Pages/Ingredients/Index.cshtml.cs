using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Data;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Pages.Ingredients // Add the correct namespace here
{
    public class IndexModel : PageModel
    {
        private readonly RecipeManagerAppContext _context;

        public IndexModel(RecipeManagerAppContext context)
        {
            _context = context;
        }

        public IList<Ingredient> Ingredients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? RecipeID { get; set; } // Selected Recipe ID

        public SelectList Recipes { get; set; } = default!; // Dropdown of Recipes

        public async Task OnGetAsync()
        {
            // Fetch recipes for the dropdown
            Recipes = new SelectList(await _context.Recipes.ToListAsync(), "ID", "Title");

            // Base query
            var ingredientsQuery = _context.Ingredients.Include(i => i.Recipe).AsQueryable();

            // Filter by RecipeID if selected
            if (RecipeID.HasValue)
            {
                ingredientsQuery = ingredientsQuery.Where(i => i.RecipeID == RecipeID);
            }

            // Execute query
            Ingredients = await ingredientsQuery.ToListAsync();
        }
    }
}
