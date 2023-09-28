using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWriting.Context;
using PersonalWriting.Model;

namespace PersonalWriting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldBuildingCategoriesController : ControllerBase
    {
        private readonly DbConnection _context;

        public WorldBuildingCategoriesController(DbConnection context)
        {
            _context = context;
        }

        // GET: api/WorldBuildingCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorldBuildingCategory>>> GetWorldBuildingCategories()
        {
          if (_context.WorldBuildingCategories == null)
          {
              return NotFound();
          }
            return await _context.WorldBuildingCategories.ToListAsync();
        }

        // GET: api/WorldBuildingCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorldBuildingCategory>> GetWorldBuildingCategory(int id)
        {
          if (_context.WorldBuildingCategories == null)
          {
              return NotFound();
          }
            var worldBuildingCategory = await _context.WorldBuildingCategories.FindAsync(id);

            if (worldBuildingCategory == null)
            {
                return NotFound();
            }

            return worldBuildingCategory;
        }

        // PUT: api/WorldBuildingCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorldBuildingCategory(int id, WorldBuildingCategory worldBuildingCategory)
        {
            if (id != worldBuildingCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(worldBuildingCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldBuildingCategoryExists(id))
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

        // POST: api/WorldBuildingCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WorldBuildingCategory>> PostWorldBuildingCategory(WorldBuildingCategory worldBuildingCategory)
        {
          if (_context.WorldBuildingCategories == null)
          {
              return Problem("Entity set 'DbConnection.WorldBuildingCategories'  is null.");
          }
            _context.WorldBuildingCategories.Add(worldBuildingCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorldBuildingCategory", new { id = worldBuildingCategory.Id }, worldBuildingCategory);
        }

        // DELETE: api/WorldBuildingCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorldBuildingCategory(int id)
        {
            if (_context.WorldBuildingCategories == null)
            {
                return NotFound();
            }
            var worldBuildingCategory = await _context.WorldBuildingCategories.FindAsync(id);
            if (worldBuildingCategory == null)
            {
                return NotFound();
            }

            _context.WorldBuildingCategories.Remove(worldBuildingCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorldBuildingCategoryExists(int id)
        {
            return (_context.WorldBuildingCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
