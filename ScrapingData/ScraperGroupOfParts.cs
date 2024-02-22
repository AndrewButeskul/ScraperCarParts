using HtmlAgilityPack;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public class ScraperGroupOfParts : IScraperData<GroupOfParts>
    {
        //private readonly HttpClient httpClient;
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;        

        public List<GroupOfParts> groupOfParts;

        public ScraperGroupOfParts(string url)
        {
            web = new();
            document = web.Load(url);
            groupOfParts = new();
        }
        public List<GroupOfParts> GetScrapingData()
        {
            var nameNodes = document.DocumentNode.SelectNodes("//*[@class='List']");
            if(nameNodes != null)
            {
                foreach (var nameNode in nameNodes)
                {
                    groupOfParts.Add(new GroupOfParts
                    {
                        GroupName = nameNode.InnerText.Trim(),
                        Url = nameNode.SelectSingleNode(".//div[@class='name']/a").GetAttributeValue("href", "")
                    });
                }
            }
            return groupOfParts;
        }
    }
}
