using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWriting.Context;
using PersonalWriting.DTO.Chapters;
using PersonalWriting.DTO.WorldBuildingDetails;
using PersonalWriting.Model;

namespace PersonalWriting.Controllers
{
    [Route("api/wb")]
    [ApiController]
    public class WorldBuildingDetailsController : ControllerBase
    {
        private readonly DbConnection _context;

        public WorldBuildingDetailsController(DbConnection context)
        {
            _context = context;
        }

        // GET: api/WorldBuildingDetails
        [HttpGet]
        public async Task<ActionResult<List<GetWorldBuildingDetails>>> GetWorldBuildingDetail()
        {
            var wbd = await _context.WorldBuildingDetail.ToListAsync();

            List<GetWorldBuildingDetails> wbdDto = new List<GetWorldBuildingDetails>();

            foreach (var w in wbd)
            {
                wbdDto.Add(new GetWorldBuildingDetails()
                {
                    Id = w.Id,
                    BookId = w.BookId,
                    Name = w.Name,
                    Content = w.Content,
                    WbCategoryId = w.WorldBuildingCategoryID,
                    WbCategoryName = w.WorldBuildingCategoryName
                });
            }

            return wbdDto;
        }

        // GET: api/WorldBuildingDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorldBuildingDetail>> GetWorldBuildingDetail(int id)
        {
            var wbd = await _context.WorldBuildingDetail
                .Where(wb => wb.Id == id)
                .FirstOrDefaultAsync();

            if(wbd == null) return NotFound();

            return wbd;
        }

        // GET: api/WorldBuildingDetails/5
        [HttpGet("byBook/{id}")]
        public async Task<ActionResult<List<GetWorldBuildingDetails>>> GetWbByBook(int id)
        {
            var wbd = await _context.WorldBuildingDetail
                .Where(wb => wb.BookId == id)
                .ToListAsync();

            List<GetWorldBuildingDetails> wbdDto = new List<GetWorldBuildingDetails>();

            foreach (var w in wbd)
            {
                wbdDto.Add(new GetWorldBuildingDetails()
                {
                    Id = w.Id,
                    BookId = w.BookId,
                    Name = w.Name,
                    Content = w.Content,
                    WbCategoryId = w.WorldBuildingCategoryID,
                    WbCategoryName = w.WorldBuildingCategoryName
                });
            }

            return wbdDto;
        }

        [HttpGet("GetTotalItemsInArray")]
        public async Task<ActionResult<List<GetWorldBuildingDetails>>> GetTotalItemsInArray([FromQuery] int[] listOfIds)
        {
            var wb = await _context.WorldBuildingDetail
                .Where(c => listOfIds.Contains(c.BookId))
                .ToListAsync();

            List<GetWorldBuildingDetails> wbDto = new List<GetWorldBuildingDetails>();

            foreach (var worldBuilding in wb)
            {
                wbDto.Add(new GetWorldBuildingDetails()
                {
                    Id = worldBuilding.Id,
                    BookId = worldBuilding.BookId,
                    Name = worldBuilding.Name,
                    Content = worldBuilding.Content,
                    WbCategoryId= worldBuilding.WorldBuildingCategoryID,
                    WbCategoryName= worldBuilding.WorldBuildingCategoryName
                });
            }
            return wbDto;
        }

        // PUT: api/WorldBuildingDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorldBuildingDetail(int id, WorldBuildingDetail worldBuildingDetail)
        {
            if (id != worldBuildingDetail.Id)
            {
                return BadRequest();
            }

            var category = await _context.WorldBuildingCategories.Where(wbc => wbc.Id == worldBuildingDetail.WorldBuildingCategoryID).FirstOrDefaultAsync();
            if (category == null) return BadRequest("Category does not exist");
            var categoryName = category.Name;
            worldBuildingDetail.WorldBuildingCategoryName = categoryName;


            _context.Entry(worldBuildingDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorldBuildingDetailExists(id))
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

        // POST: api/WorldBuildingDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostWorldBuildingDetailsDTO>> PostWorldBuildingDetail(PostWorldBuildingDetailsDTO worldBuildingDetailDTO)
        {
            var category = await _context.WorldBuildingCategories.Where(wbc => wbc.Id == worldBuildingDetailDTO.WorldBuildingCategoryID).FirstOrDefaultAsync();
            if (category == null) return BadRequest("Category does not exist");
            var categoryName = category.Name;

            WorldBuildingDetail wbd = new WorldBuildingDetail() 
            {
                Name = worldBuildingDetailDTO.Name,
                Content = worldBuildingDetailDTO.Content,
                BookId = worldBuildingDetailDTO.BookId,
                WorldBuildingCategoryID = worldBuildingDetailDTO.WorldBuildingCategoryID,
                WorldBuildingCategoryName = categoryName
            };
            _context.WorldBuildingDetail.Add(wbd);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "World building detail successfully created" });
        }

        // DELETE: api/WorldBuildingDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorldBuildingDetail(int id)
        {
            if (_context.WorldBuildingDetail == null)
            {
                return NotFound();
            }
            var worldBuildingDetail = await _context.WorldBuildingDetail.FindAsync(id);
            if (worldBuildingDetail == null)
            {
                return NotFound();
            }

            _context.WorldBuildingDetail.Remove(worldBuildingDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorldBuildingDetailExists(int id)
        {
            return (_context.WorldBuildingDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
