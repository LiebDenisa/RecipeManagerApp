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
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly RecipeManagerApp.Data.RecipeManagerAppContext _context;

        public EditModel(RecipeManagerApp.Data.RecipeManagerAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        [BindProperty]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Recipe = await _context.Recipes
                .Include(r => r.Ingredients) // Include ingredients
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Recipe == null)
            {
                return NotFound();
            }

            // Load ingredients into the list
            Ingredients = Recipe.Ingredients.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attach the updated recipe to the context
            _context.Attach(Recipe).State = EntityState.Modified;

            // Handle ingredients
            var existingIngredients = _context.Ingredients.Where(i => i.RecipeID == Recipe.ID).ToList();

            // Remove ingredients no longer in the list
            foreach (var ingredient in existingIngredients)
            {
                if (!Ingredients.Any(i => i.ID == ingredient.ID))
                {
                    _context.Ingredients.Remove(ingredient);
                }
            }

            // Update or add new ingredients
            foreach (var ingredient in Ingredients)
            {
                if (ingredient.ID == 0)
                {
                    // New ingredient
                    ingredient.RecipeID = Recipe.ID;
                    _context.Ingredients.Add(ingredient);
                }
                else
                {
                    // Existing ingredient
                    _context.Attach(ingredient).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.ID == id);
        }
    }
}
