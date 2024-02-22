using HtmlAgilityPack;
using ScrapingData.Models;

namespace ScrapingData
{
    public class ScraperEquipment : IScraperData<Equipment>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;
        public List<Equipment> equipments;
        private readonly HtmlNode firstTable;
        public ScraperEquipment(string url, string path)
        {
            web = new HtmlWeb();
            document = web.Load(url);
            equipments = new List<Equipment>();
            //firstTable = document.DocumentNode.Descendants("table").FirstOrDefault(); // using LINQ
            firstTable = document.DocumentNode.SelectSingleNode(path); 
        }
        public List<Equipment> GetScrapingData()
        {
            foreach(var node in firstTable.Descendants("tr").Skip(1))
            {
                equipments.Add(
                    new Equipment()
                    { 
                        EquipmentCode = node.SelectSingleNode("td[1]").InnerText.Trim(),
                        Date = node.SelectSingleNode("td[2]").InnerText.Trim(),
                        Engine = node.SelectSingleNode("td[3]").InnerText.Trim(),
                        Body = node.SelectSingleNode("td[4]").InnerText.Trim(),
                        Grade = node.SelectSingleNode("td[5]").InnerText.Trim(),
                        Atm = node.SelectSingleNode("td[6]").InnerText.Trim(),
                        GearShiftType = node.SelectSingleNode("td[7]").InnerText.Trim(),
                        Cab = node.SelectSingleNode("td[8]").InnerText.Trim(),
                        TransmissionModel = node.SelectSingleNode("td[9]").InnerText.Trim(),
                        LoadingCapacity = node.SelectSingleNode("td[10]").InnerText.Trim(),
                        RearTire = node.SelectSingleNode("td[11]").InnerText.Trim(),
                        Destination = node.SelectSingleNode("td[12]").InnerText.Trim(),
                        FuelInduction = node.SelectSingleNode("td[13]").InnerText.Trim(),
                        BuildingCondition = node.SelectSingleNode("td[14]").InnerText .Trim()
                    });
            }
            return equipments;
        }
        
    }
}
