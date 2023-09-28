using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalWriting.Context;
using PersonalWriting.DTO.Characters;
using PersonalWriting.DTO.Skills;
using PersonalWriting.Model;

namespace PersonalWriting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly DbConnection _context;
        private readonly IMapper mapper;

        public CharactersController(DbConnection context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Characters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }
            return await _context.Characters.ToListAsync();
        }

        [HttpGet("simple",Name = "GetCharactersSimple")]
        public async Task<ActionResult<List<GetCharacterSimpleDTO>>> GetCharactersSimple()
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters.ToListAsync();

            List<GetCharacterSimpleDTO> getCharacterSimple = new List<GetCharacterSimpleDTO>();

            foreach (var character in characters)
            {
                getCharacterSimple.Add(new GetCharacterSimpleDTO()
                {
                    Id = character.Id,
                    Name = string.Format("{0} {1}", character.FirstName, character.LastName)
                });
            }

            return getCharacterSimple;
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetCharacter(int id)
        {
          if (_context.Characters == null)
          {
              return NotFound();
          }
            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        [HttpGet("{id}/skills", Name = "GetCharacterSkills")]
        public async Task<ActionResult<List<GetCharacterSkills>>> GetSkills(int id)
        {
            var skills = await _context.Skills.Where(s => s.CharacterId == id).ToListAsync();

            if (skills == null) return NotFound("No skills found for the given character");

            List<GetCharacterSkills> skillsDTO = new List<GetCharacterSkills>();

            foreach (var skill in skills) 
            {
                skillsDTO.Add( new GetCharacterSkills()
                {
                    Id = skill.Id,
                    CharacterId = skill.CharacterId,
                    Name = skill.Name,
                    Rank = skill.Rank,
                    Description = skill.Description
                });
            }

            return skillsDTO;
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<List<GetCharacterBooks>>> GetBooks(int id)
        {
            var booksCharacters = await _context.BookCharacters.Where(bc => bc.CharacterId == id).ToListAsync();
            List<int> booksIds = booksCharacters.Select(bc => bc.BookId).ToList();
            var books = await _context.Books.Where(b => booksIds.Contains(b.Id)).ToListAsync();

            if (!books.Any()) return NotFound("No books found for given character");

            List<GetCharacterBooks> cBooks = new List<GetCharacterBooks>();

            foreach (var bc in books)
            {
                cBooks.Add(new GetCharacterBooks()
                {
                    BookId = bc.Id,
                    BookTitle = bc.Title,
                });
            }

            return cBooks;
        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, PostCharacterDTO characterDTO)
        {
            if (id != characterDTO.Id) return BadRequest();

            if(!characterDTO.BookIds.Any()) return BadRequest();

            var books = await _context.Books
            .Where(bdb => characterDTO.BookIds.Contains(bdb.Id))
            .ToListAsync();

            var bookChar = new List<BookCharacter>();

            foreach (var book in books)
            {
                _context.BookCharacters.Add(new BookCharacter() 
                { 
                    BookId = book.Id,
                    CharacterId = characterDTO.Id,
                });
            }

            var character = mapper.Map<Character>(characterDTO);

            _context.Entry(character).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(id))
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCharacter([FromBody] PostCharacterDTO postCharacterDTO)
        {
            if(postCharacterDTO.BookIds == null) return BadRequest("Characters must have book / books");
            /* var bookIds = await _context.Books
                .Where(bdb => postCharacterDTO.BookIds.Contains(bdb.Id))
                .Select(x => x.Id)
                .ToListAsync();

            if (postCharacterDTO.BookIds.Count != bookIds.Count) return BadRequest("The book sent does not exist");*/

            var character = mapper.Map<Character>(postCharacterDTO);

            _context.Characters.Add(character);

            await _context.SaveChangesAsync();
            return Ok(new
            {
                Id = character.Id
            });
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (_context.Characters == null)
            {
                return NotFound();
            }
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharacterExists(int id)
        {
            return (_context.Characters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
