using HtmlAgilityPack;
using ScrapingData;
using System.Globalization;

string url = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";

var web = new HtmlWeb();

// parsing its HTML content

var document = web.Load(url);


var nodes = document.DocumentNode.
    //SelectNodes("//*[@id=\"Body\"]/div/div[position() >= 1]/div[position()>=1]");
    SelectNodes("//*[@id=\"Body\"]/div/div[position() >= 1]/div[position()>=1]");

List<Model> models = new();

// *** running ***

//ScrapingData(models, nodes); 

string toyotaEquipmentsUrl = "https://www.ilcats.ru/toyota/?function=getComplectations&market=EU&model=671440&startDate=198308&endDate=198903";
string toyotaEquipmentsPath = "//*[@id=\"Body\"]/table[1]/tbody/tr[position()>1]"; 

ScraperEquipment scraperEquipment = new(toyotaEquipmentsUrl, toyotaEquipmentsPath);

var eq = scraperEquipment.GetScrapingData();

foreach (var e in eq)
{
    Console.WriteLine($"{e.EquipmentCode} {e.Date} {e.Engine} {e.Body} {e.Grade} {e.GearShiftType}");
}
static void ScrapingData(List<Model> models, HtmlNodeCollection nodes)
{
    foreach (var node in nodes)
    {
        models.Add(
            new Model()
            {
                ModelName = HtmlEntity.DeEntitize(node.SelectSingleNode("div[1]")?.InnerText),
                ModelCode = Convert.ToInt32(HtmlEntity.DeEntitize(node.SelectSingleNode("div[2]/div/div[1]").InnerText)),
                StartDate = GetDateFromString(HtmlEntity.DeEntitize(node.SelectSingleNode("div[2]/div/[2]").InnerText)),
                EndDate = GetDateFromString(HtmlEntity.DeEntitize(node.SelectSingleNode("div[2]/div/[2]").InnerText)),
                SpecificationName = HtmlEntity.DeEntitize(node.SelectSingleNode("div[2]/div/[3]").InnerText)
                });
    }
    
}

static DateTime GetDateFromString(string str)
{
    var isValid = DateTime.TryParseExact(str, "MM.yyyy",
                                        CultureInfo.CreateSpecificCulture("en-US"),
                                        DateTimeStyles.None,
                                        out DateTime date);
    return isValid ? date : DateTime.MinValue;
}

