using HtmlAgilityPack;
using ScrapingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrapingData.Scrapers
{
    public class ScraperParts : IScraperData<Part>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;

        public List<Part> parts;
        public ScraperParts(string url)
        {
            web = new();
            document = web.Load(url);
            parts = new();
        }
        public string GetScraperInfo() => $"Count: {parts.Count}";

        public List<Part> GetScrapingData()
        {
            var partNodes = document.DocumentNode.SelectNodes("//*[@class='Info']/table[1]/tr[th/@colspan='4']");

            if (partNodes != null)
            {
                foreach (var partNode in partNodes)
                {
                    var part = new Part
                    {
                        PartTreeCode = partNode.SelectSingleNode("th").InnerText.Trim().Split('&')[0],
                        NameTree = partNode.SelectSingleNode("th").InnerText.Trim().Substring(partNode.SelectSingleNode("th").InnerText.Trim().IndexOf(' ') + 1),
                        SubParts = new List<SubPart>()
                    };

                    var partClass = partNode.GetAttributeValue("class", "");
                    var match = Regex.Match(partClass, @"TR-(\d+)([A-Z]?)");
                    var partNumber = match.Groups[1].Value;
                    var partSuffix = match.Groups[2].Value;

                    var subPartNodes = partNode.SelectNodes($@"following-sibling::tr[
                                            starts-with(@class, 'Active TR-{partNumber}') and 
                                            not(following-sibling::th) and
                                            (
                                                @class = 'Active TR-{partNumber}' or
                                                @class = 'Active TR-{partNumber}{partSuffix}'
                                            )
                                         ]");

                    if (subPartNodes != null)
                    {
                        foreach (var subPartNode in subPartNodes)
                        {
                            var subPart = new SubPart
                            {
                                PartCode = Regex.Match(subPartNode.SelectSingleNode("td[1]").InnerText.Trim(), @"\d+$").Value,
                                Count = subPartNode.SelectSingleNode("td[2]").InnerText.Trim(),
                                DateRange = subPartNode.SelectSingleNode("td[3]").InnerText.Trim(),
                                Info = subPartNode.SelectSingleNode("td[4]").InnerText.Trim()
                            };
                            part.SubParts.Add(subPart);
                        }
                    }
                    parts.Add(part);

                }
            }

            return parts;
        }
    }
}
