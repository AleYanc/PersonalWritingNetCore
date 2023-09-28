using PersonalWriting.DTO.Chapters;
using PersonalWriting.DTO.Characters;
using PersonalWriting.DTO.WorldBuildingDetails;

namespace PersonalWriting.DTO.Books
{
    public class GetBookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? CoverUrl { get; set; }
        public List<GetChaptersDTO>? Chapters { get; set; }
        public List<GetWorldBuildingDetails>? WorldBuildings { get; set; }
        public List<GetCharacterDTO>? Characters { get; set; }
    }
}
