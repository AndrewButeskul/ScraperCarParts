using HtmlAgilityPack;
using ScrapingData.Models;
using static ScrapingData.Constants.Constants;


namespace ScrapingData.Scrapers
{
    public class ScraperEquipment : IScraperData<Equipment>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;
        public List<Equipment> equipments;

        public ScraperEquipment(string url)
        {
            web = new HtmlWeb();
            document = web.Load(url);
            equipments = new List<Equipment>();
        }

        public string GetScraperInfo() => $"Count: {equipments.Count}";

        public List<Equipment> GetScrapingData()
        {
            var firstTable = document.DocumentNode.SelectSingleNode("//*[@class='ifTableBody']/table[1]");
            foreach (var node in firstTable.Descendants("tr").Skip(1))
            {
                try
                {
                    equipments.Add(
                        new Equipment()
                        {
                            EquipmentCode = node.SelectSingleNode("td[1]").InnerText.Trim(),
                            Url = string.Concat(preffixURL, node.SelectSingleNode("td[1]/div/a").GetAttributeValue("href", "")),
                            Date = node.SelectSingleNode("td[2]")?.InnerText.Trim(),
                            Engine = node.SelectSingleNode("td[3]")?.InnerText.Trim(),
                            Body = node.SelectSingleNode("td[4]")?.InnerText.Trim(),
                            Grade = node.SelectSingleNode("td[5]")?.InnerText.Trim(),
                            Atm = node.SelectSingleNode("td[6]")?.InnerText.Trim(),
                            GearShiftType = node.SelectSingleNode("td[7]")?.InnerText.Trim(),
                            Cab = node.SelectSingleNode("td[8]")?.InnerText.Trim(),
                            TransmissionModel = node.SelectSingleNode("td[9]")?.InnerText.Trim(),
                            LoadingCapacity = node.SelectSingleNode("td[10]")?.InnerText.Trim(),
                            RearTire = node.SelectSingleNode("td[11]")?.InnerText.Trim(),
                            Destination = node.SelectSingleNode("td[12]")?.InnerText.Trim(),
                            FuelInduction = node.SelectSingleNode("td[13]")?.InnerText.Trim(),
                            BuildingCondition = node.SelectSingleNode("td[14]")?.InnerText.Trim()
                        });
                }
                catch(NullReferenceException ex) 
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return equipments;
        }

    }
}
