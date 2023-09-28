namespace PersonalWriting.DTO.WorldBuildingDetails
{
    public class PostWorldBuildingDetailsDTO
    {
        public int BookId { get; set; }
        public string? Content { get; set; }
        public string? Name { get; set; }
        public int WorldBuildingCategoryID { get; set; }
        public string? WorldBuildingCategoryName { get; set; }
    }
}
