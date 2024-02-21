using HtmlAgilityPack;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    internal class ScraperModel : IScraperData<ModelName>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;
        private readonly HtmlNodeCollection modelDataLists;

        public List<ModelName> modelNames;
        public List<Model> modelData;
       
        public ScraperModel(string html)
        {
            web = new();
            document = web.Load(html);

            modelNames = new();
            modelData = new();
            modelDataLists = document.DocumentNode.SelectNodes("//*[@class='List Multilist']/div/div[2]/div");
        }
        public List<ModelName> GetScrapingData()
        {
            GetModelNames();
            foreach (var modelDataList in modelDataLists)
            {
                var currentModelName = modelDataList
                    .Ancestors("div").First(d => d.HasClass("Header"))
                    .SelectSingleNode(".//div[@class='name']").InnerText;


                var modelNameIndex = modelNames.FindIndex(m => m.Name == currentModelName);

                foreach(var modelNode in modelDataList.SelectNodes(".//div[@class='List']"))
                {
                    modelData.Add(new Model()
                    {
                        ModelCode = modelNode.SelectSingleNode(".//div[@class='id']/a").InnerText,
                        DateRange = modelNode.SelectSingleNode(".//div[@class='dateRange']").InnerText.Trim(),
                        SpecificationName = modelNode.SelectSingleNode(".//div[@class='modelCode']").InnerText.Trim(),
                        ModelNameId = (modelNameIndex >= 0) ? modelNames[modelNameIndex].ModelNameId : -1
                    });
                }

            }

            return modelNames;
        }

        /// <summary>
        /// Extract all model names
        /// </summary>
        private void GetModelNames()
        {
            foreach (var nameNode in document.DocumentNode.
                SelectNodes("//*[@class='List Multilist']/div/div[1]"))
            {
                modelNames.Add(new ModelName()
                {
                    Name = nameNode.SelectSingleNode(".//div[@class='name']").InnerText
                });
            }
        }

    }
}
