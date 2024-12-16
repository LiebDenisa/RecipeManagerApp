using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeManagerApp.Data;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Pages.Recipes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly RecipeManagerApp.Data.RecipeManagerAppContext _context;

        public CreateModel(RecipeManagerApp.Data.RecipeManagerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        [BindProperty]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attach ingredients to the recipe
            if (Ingredients != null && Ingredients.Count > 0)
            {
                Recipe.Ingredients = Ingredients;
            }

            _context.Recipes.Add(Recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
