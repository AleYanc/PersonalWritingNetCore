namespace PersonalWriting.DTO.WorldBuildingDetails
{
    public class GetWorldBuildingDetails
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public int WbCategoryId { get; set; }
        public string? WbCategoryName { get; set; }
    }
}
