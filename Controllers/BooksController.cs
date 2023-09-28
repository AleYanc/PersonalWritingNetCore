using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWriting.Context;
using PersonalWriting.DTO.Books;
using PersonalWriting.DTO.Chapters;
using PersonalWriting.DTO.Characters;
using PersonalWriting.DTO.WorldBuildingDetails;
using PersonalWriting.DTO.WorldBuildings;
using PersonalWriting.Model;

namespace PersonalWriting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DbConnection _context;

        public BooksController(DbConnection context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<GetBooksDTO>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            List<GetBooksDTO> result = new List<GetBooksDTO>();
            foreach (var book in books)
            {
                result.Add(new GetBooksDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    CoverUrl = book.CoverUrl,
                });
            }

            return result;
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ActionResult<GetBookDTO>> GetBook(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(bdb => bdb.Id == id);
            var chapters = _context.Chapters.Where(c => c.BookID == id);
            List<GetChaptersDTO> chaptersList = new List<GetChaptersDTO>();

            foreach (var chapter in chapters)
            {
                chaptersList.Add(new GetChaptersDTO()
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    BookID = chapter.BookID,
                });
            }

            if (book == null) { return new GetBookDTO(); }
            GetBookDTO result = new GetBookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                CoverUrl = book.CoverUrl,
                Chapters = chaptersList
            };

            return result;
        }

        [HttpGet("{id}/wb", Name = "GetBookWorldBuilding")]
        public async Task<ActionResult<List<GetWorldBuildingDetails>>> GetBookWBD(int id)
        {
            List<GetWorldBuildingDetails> wb = new List<GetWorldBuildingDetails>();
            var wbDetails = await _context.WorldBuildingDetail.Where(wbd => wbd.BookId == id).ToListAsync();
            foreach (var wbDetail in wbDetails)
            {
                wb.Add(new GetWorldBuildingDetails()
                {
                    Id = wbDetail.Id,
                    Name = wbDetail.Name,
                    Content = wbDetail.Content,
                    WbCategoryId = wbDetail.WorldBuildingCategoryID,
                    WbCategoryName = wbDetail.WorldBuildingCategoryName
                });
            }

            return wb;
        }

        [HttpGet("{id}/characters", Name = "GetBookCharacters")]
        public async Task<List<GetCharacterDTO>> GetBookCharacters(int id)
        {
            var bookCharacter = await _context.BookCharacters
                .Where(t => t.BookId == id)
                .ToListAsync();
            List<int> ids = new List<int>();
            foreach (var charId in bookCharacter)
            {
                ids.Add(charId.CharacterId);
            }
            var characters = await _context.Characters.Where(c => ids.Contains(c.Id)).ToListAsync();

            if (bookCharacter == null) return new List<GetCharacterDTO>();

            List<GetCharacterDTO> charactersList = new List<GetCharacterDTO>();
            foreach (var character in characters)
            {
                charactersList.Add(new GetCharacterDTO()
                {
                    Id = character.Id,
                    FirstName = character.FirstName,
                    MiddleName = character.MiddleName,
                    LastName = character.LastName,
                    ImageUrl = character.ImageUrl,
                    Age = character.Age,
                    Birthday = character.Birthday,
                    EyeColor = character.EyeColor,
                    HairColor = character.HairColor,
                    SkinColor = character.SkinColor,
                    Weight = character.Weight,
                    Height = character.Height,
                    GoodTraits = character.GoodTraits,
                    BadTraits = character.BadTraits,
                    MainFear = character.MainFear,
                    MainWish = character.MainWish,
                    Description = character.Description,
                    Notes = character.Notes
                });
            }

            return charactersList;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PostBookDTO>> PostBook([FromBody] PostBookDTO bookDTO)
        {
            Book book = new Book()
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                CoverUrl = bookDTO.CoverUrl,
            };

            _context.Books.Add(book);

            await _context.SaveChangesAsync();
            return Ok(new
            {
                Id = book.Id,
            });
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Book deleted"
            });
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
