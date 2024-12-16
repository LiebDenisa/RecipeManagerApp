using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeManagerApp.Data;
using RecipeManagerApp.Models;

namespace RecipeManagerApp.Pages.Ingredients
{
    [Authorize(Roles = "Admin")] 
    public class CreateModel : PageModel
    {
        private readonly RecipeManagerApp.Data.RecipeManagerAppContext _context;

        public CreateModel(RecipeManagerApp.Data.RecipeManagerAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RecipeID"] = new SelectList(_context.Recipes, "ID", "Description");
            return Page();
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Ingredients.Add(Ingredient);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
