using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeShare.Data;
using RecipeShare.Models;

namespace RecipeShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeShareController : ControllerBase
    {
        private readonly RecipeShareContext _context;

        public RecipeShareController(RecipeShareContext context)
        {
            _context = context;
        }

        // GET: api/RecipeShare
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            return await _context.Recipe.ToListAsync();
        }

        // GET: api/RecipeShare/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/RecipeShare/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.ID)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RecipeShare
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipe.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.ID }, recipe);
        }

        // DELETE: api/RecipeShare/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/RecipeShare/filter/Vegan
        [HttpGet("filter/{tag}")]
        [Tags("Filter")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetFilter(string tag)
        {
            var recipes = await _context.Recipe
                .Where(r => r.DietaryTags != null && r.DietaryTags.ToLower().Contains(tag.ToLower()))
                .ToListAsync();

            if (recipes.Count() == 0)
            {
                return NotFound();
            }

            return recipes;
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.ID == id);
        }
    }
}
