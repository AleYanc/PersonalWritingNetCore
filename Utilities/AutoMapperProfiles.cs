using AutoMapper;
using PersonalWriting.DTO.Characters;
using PersonalWriting.Model;

namespace PersonalWriting.Utilities
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            // Book mapping

            // Character mapping
            CreateMap<PostCharacterDTO, Character>()
                .ForMember(book => book.Books, opt => opt.MapFrom(MapBookCharacterPost));
        }

        private List<BookCharacter> MapBookCharacterPost(PostCharacterDTO postCharDTO, Character character)
        {
            var result = new List<BookCharacter>();

            if (postCharDTO.BookIds == null) return result;

            foreach (var bookId in postCharDTO.BookIds)
            {
                result.Add(new BookCharacter() { BookId = bookId });
            }

            return result;
        }
    }
}
