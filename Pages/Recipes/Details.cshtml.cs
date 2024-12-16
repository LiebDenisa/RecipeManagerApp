using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipeManagerApp.Data;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Pages.Recipes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly RecipeManagerApp.Data.RecipeManagerAppContext _context;

        public DetailsModel(RecipeManagerApp.Data.RecipeManagerAppContext context)
        {
            _context = context;
        }

        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Ingredients) // Ensure ingredients are included
                .FirstOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            else
            {
                Recipe = recipe;
            }
            return Page();
        }
    }
}
