﻿using HtmlAgilityPack;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public class ScraperModel : IScraperData<ModelName>
    {
        private readonly HtmlDocument document;

        public List<ModelName> modelNames;
        public List<Model> modelData;
       
        public ScraperModel(string mainHtml)
        {
            document = new();
            document.LoadHtml(mainHtml);
            modelNames = new();
        }
        public List<ModelName> GetScrapingData()
        {
            var nameNodes = document.DocumentNode.SelectNodes("//*[@class='List Multilist']//div[@class='Header']//div[@class='name']");

            if (nameNodes != null)
            {
                foreach (var nameNode in nameNodes)
                {
                    var modelName = new ModelName
                    {
                        Name = nameNode.InnerText.Trim(),
                        Models = new List<Model>()
                    };

                    var modelDataNodes = nameNode.ParentNode.ParentNode.SelectNodes(".//div[@class='List']");
                    if(modelDataNodes != null)
                    {
                        foreach(var modelDataNode in modelDataNodes)
                        {
                            var modelData = new Model
                            {
                                ModelCode = modelDataNode.SelectSingleNode(".//div[@class='id']/a").InnerText.Trim(),
                                DateRange = modelDataNode.SelectSingleNode(".//div[@class='dateRange']").InnerText.Trim(),
                                Url = modelDataNode.SelectSingleNode(".//div[@class='id']/a").GetAttributeValue("href", ""),
                                SpecificationName = modelDataNode.SelectSingleNode(".//div[@class='modelCode']").InnerText.Trim()
                            };
                            modelName.Models.Add(modelData);
                        }
                    }
                    modelNames.Add(modelName);
                }
            }
            return modelNames;
        }
    }
}
