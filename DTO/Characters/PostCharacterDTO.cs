namespace PersonalWriting.DTO.Characters
{
    public class PostCharacterDTO
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? Birthday { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? SkinColor { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public string? GoodTraits { get; set; }
        public string? BadTraits { get; set; }
        public string? MainFear { get; set; }
        public string? MainWish { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public List<int>? BookIds { get; set; }
    }
}
