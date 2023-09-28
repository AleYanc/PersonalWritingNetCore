namespace PersonalWriting.DTO.Skills
{
    public class GetSkill
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string? Name { get; set; }
        public string? Rank { get; set; }
        public string? Description { get; set; }
    }
}
