﻿using HtmlAgilityPack;
using ScrapingData.Contracts;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ScrapingData.Constants.Constants;

namespace ScrapingData.Scrapers
{
    public class ScraperModel : IScraperData<ModelName>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;

        public List<ModelName> modelNames;
        public List<SubModel> modelData;

        public ScraperModel(string url)
        {
            web = new HtmlWeb();
            document = web.Load(url);
            modelNames = new();
        }

        public string GetScraperInfo() => $"Count: {modelNames.Count}";

        public List<ModelName> GetScrapingData()
        {
            var nameNodes = document.DocumentNode.SelectNodes("//*[@class='List Multilist']//div[@class='Header']//div[@class='name']");

            if (nameNodes != null)
            {
                foreach (var nameNode in nameNodes)
                {
                    if (modelNames.Count > 4)
                        break; // only first 4 models

                    var modelName = new ModelName
                    {
                        Name = nameNode.InnerText.Trim(),
                        SubModels = new List<SubModel>()
                    };

                    var modelDataNodes = nameNode.ParentNode.ParentNode.SelectNodes(".//div[@class='List']");
                    if (modelDataNodes != null)
                    {
                        foreach (var modelDataNode in modelDataNodes)
                        {
                            try
                            {
                                var modelData = new SubModel
                                {
                                    ModelCode = modelDataNode.SelectSingleNode(".//div[@class='id']/a").InnerText.Trim(),
                                    DateRange = modelDataNode.SelectSingleNode(".//div[@class='dateRange']").InnerText.Trim(),
                                    Url = string.Concat(preffixURL, modelDataNode.SelectSingleNode(".//div[@class='id']/a").GetAttributeValue("href", "")),
                                    SpecificationName = modelDataNode.SelectSingleNode(".//div[@class='modelCode']").InnerText.Trim()
                                };
                                modelName.SubModels.Add(modelData);
                            }
                            catch(NullReferenceException ex) { Console.WriteLine(ex.Message); }
                        }
                    }
                    modelNames.Add(modelName);
                }
            }
            return modelNames;
        }
    }
}
