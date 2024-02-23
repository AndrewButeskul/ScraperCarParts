using ScrapingData.Models;

namespace ScrapingData
{
    public class Data
    {
        public ICollection<ModelName> ModelNames { get; set; }
        public ICollection<Model> ModelData { get; set; }
        public ICollection<Equipment> EquipmentData { get; set; }
        public ICollection<GroupOfParts> GroupOfPartsData { get; set; }
        public ICollection<SubGroup> SubGroupsData { get; set; }
        public ICollection<Part> PartsData { get; set; }
        public ICollection<SubPart> SubPartsData { get; set; }
    }
}