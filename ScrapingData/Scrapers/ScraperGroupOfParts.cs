using HtmlAgilityPack;
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
    public class ScraperGroupOfParts : IScraperData<GroupOfPart>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;

        public List<GroupOfPart> groupOfParts;

        public ScraperGroupOfParts(string url)
        {
            web = new();
            document = web.Load(url);
            groupOfParts = new();
        }

        public string GetScraperInfo() => $"Count: {groupOfParts.Count}";

        public List<GroupOfPart> GetScrapingData()
        {
            var nameNodes = document.DocumentNode.SelectNodes("//*[@class='List']");
            if (nameNodes != null)
            {
                foreach (var nameNode in nameNodes)
                {
                    try
                    {
                        groupOfParts.Add(new GroupOfPart
                        {
                            GroupName = nameNode.InnerText.Trim(),
                            Url = string.Concat(preffixURL, nameNode.SelectSingleNode(".//div[@class='name']/a").GetAttributeValue("href", ""))
                        });
                    }
                    catch(NullReferenceException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return groupOfParts;
        }
    }
}
