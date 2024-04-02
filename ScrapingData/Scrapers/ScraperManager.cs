using ScrapingData.Models;
using ScrapingData.Logger;

namespace ScrapingData.Scrapers
{
    public class ScraperManager
    {
        private readonly string _initUrl;
        private List<ModelName> modelsData;

        public ScraperManager(string initURL)
        {
            _initUrl = initURL;
            modelsData = new();
        }

        public List<ModelName> ScrapingData()
        {
            ScarpeModels(_initUrl);
            return modelsData;
        }

        private void ScarpeModels(string url)
        {
            var scraperModels = new ScraperModel(url);
            modelsData = scraperModels.GetScrapingData();
            Logger.Logger.LogToConsole(scraperModels);
            foreach (var model in modelsData)
            {
                foreach (var subModel in model.SubModels)
                {
                    subModel.Equipments = new List<Equipment>();
                    subModel.Equipments = ScrapeEquipments(subModel.Url);
                }
            }
        }
        private List<Equipment> ScrapeEquipments(string url)
        {
            var scraperEquipments = new ScraperEquipment(url);
            var equipments = scraperEquipments.GetScrapingData();
            Logger.Logger.LogToConsole(scraperEquipments);
            foreach (var equipment in equipments)
            {
                equipment.GroupOfParts = new List<GroupOfPart>();
                equipment.GroupOfParts = ScrapeGroupOfParts(equipment.Url);
            }
            return equipments;
        }
        private List<GroupOfPart> ScrapeGroupOfParts(string url)
        {
            var scraperGroupOfParts = new ScraperGroupOfParts(url);
            var groups = scraperGroupOfParts.GetScrapingData();
            Logger.Logger.LogToConsole(scraperGroupOfParts);
            foreach (var group in groups)
            {
                group.SubGroups = new List<SubGroup>();
                group.SubGroups = ScrapeSubGroups(group.Url);
            }
            return groups;
        }
        private List<SubGroup> ScrapeSubGroups(string url)
        {
            var scraperSubGroup = new ScraperSubGroup(url);
            var subGroups = scraperSubGroup.GetScrapingData();
            Logger.Logger.LogToConsole(scraperSubGroup);
            foreach (var subGroup in subGroups)
            {
                subGroup.Parts = new List<Part>();
                subGroup.Parts = ScrapeParts(subGroup.Url);
            }
            return subGroups;
        }

        private List<Part> ScrapeParts(string url)
        {
            var scraperParts = new ScraperParts(url);
            var parts = scraperParts.GetScrapingData();
            Logger.Logger.LogToConsole(scraperParts);
            return parts;
        }
    }
}
