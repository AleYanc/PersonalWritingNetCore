using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWriting.Context;
using PersonalWriting.DTO.Chapters;
using PersonalWriting.Model;

namespace PersonalWriting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaptersController : ControllerBase
    {
        private readonly DbConnection _context;

        public ChaptersController(DbConnection context)
        {
            _context = context;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetChaptersDTO>>> GetChapters()
        {
            if (_context.Chapters == null)
            {
                return NotFound();
            }

            var chapters = await _context.Chapters
                  .Include(c => c.Book)
                  .ToListAsync();

            List<GetChaptersDTO> chaptersDTO = new List<GetChaptersDTO>();

            foreach (var chapter in chapters)
            {
                chaptersDTO.Add(new GetChaptersDTO()
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    BookTitle = chapter?.Book?.Title,
                    ChapterNumber = chapter.ChapterNumber,
                    Created = chapter.Created
                });
            }

            return Ok(chaptersDTO);
        }

        [HttpGet("GetTotalItemsInArray")]
        public async Task<ActionResult<List<GetChaptersDTO>>> GetTotalItemsInArray([FromQuery] int[] listOfIds)
        {
            var chapters = await _context.Chapters
                .Include(c => c.Book)
                .Where(c => listOfIds
                .Contains(c.BookID)).ToListAsync();
            List<GetChaptersDTO> chaptersDTO = new List<GetChaptersDTO>();

            foreach (var chapter in chapters)
            {
                chaptersDTO.Add(new GetChaptersDTO()
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    ChapterNumber = chapter.ChapterNumber,
                    BookTitle = chapter?.Book?.Title
                });
            }

            return Ok(chaptersDTO);
        }

        [HttpGet("getNav/{bookId}/{chapterNumber}", Name = "GetChapterNav")]
        public async Task<ActionResult<List<ChapterNavDTO>>> GetChapterNavigation(int bookId, int chapterNumber)
        {
            var chapNav = await _context
                .Chapters
                    .FromSql($"spGetPrevNextChapter {bookId}, {chapterNumber}")
                    .AsNoTracking()
                    .ToListAsync();

            List<ChapterNavDTO> chapNavDTO  = new List<ChapterNavDTO>();

            foreach (var chap in chapNav)
            {
                chapNavDTO.Add(new ChapterNavDTO()
                {
                    Id = chap.Id,
                    BookId = chap.BookID,
                    ChapterNumber = chap.ChapterNumber,
                    ChapMsg = chap.ChapterNumber == chapterNumber ? "Actual chapter" : chap.ChapterNumber > chapterNumber ? "Next chapter" : "Previous chapter"
                });
            }

            return chapNavDTO;
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetChapterDTO>> GetChapter(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null) return NotFound("Chapter not found");
            GetChapterDTO chapterDTO = new GetChapterDTO()
            {
                Id = chapter.Id,
                Title = chapter.Title,
                Synopsys = chapter.Synopsys,
                ChapterNumber = chapter.ChapterNumber,
                Created = chapter.Created,
                BookID = chapter.BookID,
            };
            return chapterDTO;
        }

        [HttpGet("byBook/{id}")]
        public async Task<ActionResult<List<GetChapterDTO>>> GetChaptersByBook(int id)
        {
            var chapters = await _context.Chapters
                .Where(c => c.BookID == id)
                .ToListAsync();

            if (chapters == null) return NotFound();

            return Ok(chapters);
        }

        [HttpGet("byChapNumber/{bookId}/{chapNumber}", Name = "GetCharacterByChapNumber")]
        public async Task<ActionResult<GetChapterDTO>> GetChapterByChapNumber(int bookId, int chapNumber)
        {
            var chapter = await _context.Chapters
                .Where(c => c.ChapterNumber == chapNumber && c.BookID == bookId)
                .FirstOrDefaultAsync();
            if (chapter == null) return NotFound("Chapter not found");
            GetChapterDTO chapterDTO = new GetChapterDTO()
            {
                Id = chapter.Id
            };
            return chapterDTO;
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChapter(int id, Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            var lastChapter = await _context.Chapters
                .Where(c => c.BookID == chapter.BookID)
                .OrderByDescending(c => c.Created)
                .FirstOrDefaultAsync();

            int chapterNumber;

            if (lastChapter != null)
            {
                chapterNumber = lastChapter.ChapterNumber + 1;
            }
            else
            {
                chapterNumber = 1;
            }

            chapter.ChapterNumber = chapterNumber;

            _context.Entry(chapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
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

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostChapterDTO>> PostChapter(PostChapterDTO chapterDTO)
        {
            var book = await _context.Books.FindAsync(chapterDTO.BookID);

            if(book == null) return BadRequest()
                    ;
            var lastChapter = await _context.Chapters
                .Where(c => c.BookID == book.Id)
                .OrderByDescending(c => c.Created)
                .FirstOrDefaultAsync();

            int chapterNumber;

            if(lastChapter != null)
            {
                chapterNumber = lastChapter.ChapterNumber + 1;
            } else
            {
                chapterNumber = 1;
            }

            Chapter chapter = new Chapter()
            {
                Title = chapterDTO.Title,
                Synopsys= chapterDTO.Synopsys,
                BookID = chapterDTO.BookID,
                ChapterNumber = chapterNumber
            };

            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Chapter created successfully"
            });
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            if (_context.Chapters == null)
            {
                return NotFound();
            }
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterExists(int id)
        {
            return (_context.Chapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
