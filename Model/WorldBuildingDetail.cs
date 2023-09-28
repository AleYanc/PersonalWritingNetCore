namespace PersonalWriting.Model
{
    public class WorldBuildingDetail
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string? Name { get; set; }
        public string? Content { get; set; }
        public int WorldBuildingCategoryID { get; set; }
        public string? WorldBuildingCategoryName { get; set; } 
    }
}
