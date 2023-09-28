using PersonalWriting.Model;

namespace PersonalWriting.DTO.Chapters
{
    public class GetChapterDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Synopsys { get; set; }
        public DateTime Created { get; set; }
        public int ChapterNumber { get; set; }
        public int BookID { get; set; }
        public Book? Book { get; set; }
    }
}
