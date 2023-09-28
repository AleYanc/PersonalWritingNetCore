using PersonalWriting.Model;

namespace PersonalWriting.DTO.WorldBuildings
{
    public class GetWorldBuildings
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int WorldBuildingCategoryID { get; set; }
        public int WorldBuildingDetailID { get; set; }
        public WorldBuildingCategory? WorldBuildingCategory { get; set; }
        public WorldBuildingDetail? WorldBuildingDetail { get; set; }
    }
}
