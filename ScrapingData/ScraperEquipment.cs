using HtmlAgilityPack;

namespace ScrapingData
{
    internal class ScraperEquipment : IScraperData<Equipment>
    {
        private readonly HtmlWeb web;
        private readonly HtmlDocument document;
        private readonly HtmlNodeCollection nodes;
        public List<Equipment> equipments;

        private HtmlNode firstTable;
        public ScraperEquipment(string url, string xPath)
        {
            web = new HtmlWeb();
            document = web.Load(url);
            equipments = new List<Equipment>();
            firstTable = document.DocumentNode.Descendants("table").FirstOrDefault();
        }
        public List<Equipment> GetScrapingData()
        {
            foreach(var node in firstTable.Descendants("tr").Skip(1))
            {
                equipments.Add(
                    new Equipment()
                    {
                        EquipmentCode = node.SelectSingleNode("td[1]").InnerText,
                        Date = node.SelectSingleNode("td[2]").InnerText,
                        Engine = node.SelectSingleNode("td[3]").InnerText,
                        Body = node.SelectSingleNode("td[4]").InnerText,
                        Grade = node.SelectSingleNode("td[5]").InnerText,
                        Atm = node.SelectSingleNode("td[6]").InnerText,
                        GearShiftType = node.SelectSingleNode("td[7]").InnerText,
                        Cab = node.SelectSingleNode("td[8]").InnerText,
                        TransmissionModel = node.SelectSingleNode("td[9]").InnerText,
                        LoadingCapacity = node.SelectSingleNode("td[10]").InnerText,
                        RearTire = node.SelectSingleNode("td[11]").InnerText,
                        Destination = node.SelectSingleNode("td[12]").InnerText,
                        FuelInduction = node.SelectSingleNode("td[13]").InnerText,
                        BuildingCondition = node.SelectSingleNode("td[14]").InnerText
                    });
            }
            return equipments;
        }
        
    }
}
