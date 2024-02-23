using HtmlAgilityPack;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static ScrapingData.Constants.Constants;


namespace ScrapingData.Scrapers
{
    public class ScraperSubGroup : IScraperData<SubGroup>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;

        public List<SubGroup> subGroups;

        public ScraperSubGroup(string url)
        {
            web = new();
            document = web.Load(url);
            subGroups = new();
        }

        public List<SubGroup> GetScrapingData()
        {
            var titlesNodes = document.DocumentNode.SelectNodes("//*[@class='Tiles']//div[@class='List']");
            if (titlesNodes != null)
            {
                foreach (var title in titlesNodes)
                {
                    subGroups.Add(new SubGroup()
                    {
                        SubGroupName = title.SelectSingleNode(".//div[@class='name']").InnerText.Trim(),
                        Url = string.Concat(preffixURL,title.SelectSingleNode(".//div[@class='name']/a").GetAttributeValue("href", "").Trim())
                    });
                }
            }
            return subGroups;
        }
    }
}
