﻿namespace PersonalWriting.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public List<BookCharacter> Books { get; set; } = new();
    }
}
