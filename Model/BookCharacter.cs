namespace PersonalWriting.Model
{
    public class BookCharacter
    {
        public int BookId { get; set; }
        public int CharacterId { get; set; }
        public Book? Book { get; set; }
        public Character? Character { get; set; }
    }
}
