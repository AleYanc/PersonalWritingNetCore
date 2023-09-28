using PersonalWriting.DTO.Chapters;
using PersonalWriting.Model;

namespace PersonalWriting.DTO.Books
{
    public class GetBooksDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
    }
}
