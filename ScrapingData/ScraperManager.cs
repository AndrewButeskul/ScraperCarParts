using ScrapingData.Scrapers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public class ScraperManager
    {
        private readonly string _initURL;

        public ScraperManager(string initURL)
        {
            _initURL = initURL;
        }

        public Data ScrapingData()
        {
            var dataBuilder = new DataBuilder();
            ScarpeModels(_initURL, dataBuilder);
            var data = dataBuilder.Build();
            return data;
        }

        private void ScarpeModels(string url, DataBuilder dataBuilder)
        {
            var scraperModels = new ScraperModel(url);
            var models = scraperModels.GetScrapingData();
            dataBuilder.AddModelData(models);

            foreach(var model in models)
            {
                foreach (var modelData in model.Models)
                {
                    ScrapeEquipments(modelData.Url, dataBuilder);
                }
            }
        }
        private void ScrapeEquipments(string url, DataBuilder dataBuilder)
        {
            var scraperEquipments = new ScraperEquipment(url);
            var equipments = scraperEquipments.GetScrapingData();
            dataBuilder.AddEquipmentData(equipments);

            foreach (var equipment in equipments)
            {
                ScrapeGroupOfParts(equipment.Url, dataBuilder);
            }
        }
        private void ScrapeGroupOfParts(string url, DataBuilder dataBuilder)
        {
            var scraperGroupOfParts = new ScraperGroupOfParts(url);
            var groups = scraperGroupOfParts.GetScrapingData();
            dataBuilder.AddGroupOfPartsData(groups);

            foreach (var group in groups)
            {
                ScrapeSubGroups(group.Url, dataBuilder);
            }
        }
        private void ScrapeSubGroups(string url, DataBuilder dataBuilder)
        {
            var scraperSubGroup = new ScraperSubGroup(url);
            var subGroups = scraperSubGroup.GetScrapingData();
            dataBuilder.AddSubGroupData(subGroups);

            foreach (var subGroup in subGroups)
            {
                ScrapeParts(subGroup.Url, dataBuilder);
            }
        }

        private void ScrapeParts(string url, DataBuilder dataBuilder)
        {
            var scraperParts = new ScraperParts(url);
            var parts = scraperParts.GetScrapingData();
            dataBuilder.AddPartData(parts);
        }
    }
}
