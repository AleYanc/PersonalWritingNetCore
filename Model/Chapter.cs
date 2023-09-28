namespace PersonalWriting.Model
{
    public class Chapter
    {
        public int Id { get; set; }
        public int ChapterNumber { get; set; }
        public string? Title { get; set; }
        public string? Synopsys { get; set; }
        public int BookID { get; set; }
        public DateTime Created { get; set; }
        public Book? Book { get; set; }
    }
}
