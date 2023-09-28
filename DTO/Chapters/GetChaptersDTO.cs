namespace PersonalWriting.DTO.Chapters
{
    public class GetChaptersDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int BookID { get; set; }
        public int ChapterNumber { get; set; }
        public DateTime Created { get; set; }
        public string? BookTitle { get; set; }
    }
}
