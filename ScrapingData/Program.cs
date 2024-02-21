using HtmlAgilityPack;
using ScrapingData;
using ScrapingData.Models;
using System.Globalization;



string url = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";


// *** running ***

ScraperModel scraperModel = new(url);
var modelNames = scraperModel.GetScrapingData();

foreach (var item in modelNames)
{
    Console.WriteLine($"{item.Name}");
}


// -------------------------------------------

string toyotaEquipmentsUrl = "https://www.ilcats.ru/toyota/?function=getComplectations&market=EU&model=671440&startDate=198308&endDate=198903";
string toyotaEquipmentsPath = "//*[@class='ifTableBody']/table[1]";

ScraperEquipment scraperEquipment = new(toyotaEquipmentsUrl, toyotaEquipmentsPath);

var eq = scraperEquipment.GetScrapingData();

foreach (var e in eq)
{
    Console.WriteLine($"{e.EquipmentCode} {e.Date} {e.Engine} {e.Body} {e.Grade} {e.GearShiftType}");
}


static DateTime GetDateFromString(string str)
{
    var isValid = DateTime.TryParseExact(str, "MM.yyyy",
                                        CultureInfo.CreateSpecificCulture("en-US"),
                                        DateTimeStyles.None,
                                        out DateTime date);
    return isValid ? date : DateTime.MinValue;
}
