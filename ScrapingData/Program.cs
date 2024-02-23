using HtmlAgilityPack;
using ScrapingData;
using ScrapingData.Models;
using ScrapingData.Scrapers;
using System.Globalization;


string URL = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";

ScraperManager scraperManager = new(URL);
Data data = scraperManager.ScrapingData();
Console.WriteLine("Completed");

Console.WriteLine($"Count of Models: {data.ModelNames.Count}");
Console.WriteLine($"Count of Equipments: {data.EquipmentData.Count}");
Console.WriteLine($"Count of Group: {data.GroupOfPartsData.Count}");
Console.WriteLine($"Count of SubGroup: {data.SubPartsData.Count}");
Console.WriteLine($"Count of Parts: {data.PartsData.Count}");
Console.WriteLine($"Count of SubParts: {data.SubPartsData.Count}");

Console.WriteLine("------------------------------------------------");

foreach (var item in data.ModelData)
{
    Console.WriteLine($"Model Code: {item.ModelCode} {item.SpecificationName}");
}

Console.WriteLine("------------------------------------------------");

foreach (var item in data.EquipmentData)
{
    Console.WriteLine($"Model Code: {item.EquipmentCode}");
}
Console.WriteLine("------------------------------------------------");

foreach (var item in data.GroupOfPartsData)
{
    Console.WriteLine($"Model Code: {item.GroupName}");
}

Console.WriteLine("------------------------------------------------");

foreach (var item in data.PartsData)
{
    Console.WriteLine($"Model Code: {item.NameTree} {item.PartTreeCode}");
}
//SimpleTestScraping();
static void SimpleTestScraping()
{
    // -----------------------   Page Models   ----------------------------

    string initURL = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";

    //HttpClient httpClient = new();
    //var mainHtml = await httpClient.GetStringAsync(initURL);

    ScraperModel scraperModel = new(initURL);

    var modelNames = scraperModel.GetScrapingData();

    foreach (var modelName in modelNames)
    {
        Console.WriteLine($"{modelName.Name}");
        foreach (var model in modelName.Models)
        {
            Console.WriteLine($"{model.Name} {model.SpecificationName} {model.DateRange}");
        }
    }


    // -------------------   Page Equipments (комплектації)   ------------------------

    string toyotaEquipmentsUrl = "https://www.ilcats.ru/toyota/?function=getComplectations&market=EU&model=671440&startDate=198308&endDate=198903";
    string toyotaEquipmentsPath = "//*[@class='ifTableBody']/table[1]";

    ScraperEquipment scraperEquipment = new(toyotaEquipmentsUrl);

    var equipments = scraperEquipment.GetScrapingData();

    foreach (var e in equipments)
    {
        Console.WriteLine($"{e.EquipmentCode} {e.Date} {e.Engine} {e.Body} {e.Grade} {e.GearShiftType}");
    }

    // --------------------   Page Group of Parts   -------------------------

    ScraperGroupOfParts scraperGroupOfParts =
        new("https://www.ilcats.ru/toyota/?function=getGroups&market=EU&model=671440&modification=LN51L-KRA&complectation=001");

    var groups = scraperGroupOfParts.GetScrapingData();

    foreach (var g in groups)
    {
        Console.WriteLine($"{g.GroupName}");
    }

    Console.WriteLine();

    // ---------------------    Page Sub Group of Parts     ------------------------

    ScraperSubGroup scraperSubGroup =
        new("https://www.ilcats.ru/toyota/?function=getSubGroups&market=EU&model=671440&modification=LN51L-KRA&complectation=001&group=3");

    var subGroups = scraperSubGroup.GetScrapingData();

    foreach (var sg in subGroups)
    {
        Console.WriteLine($"{sg.SubGroupName}");
    }

    Console.WriteLine();

    // ---------------------    Page Parts     ------------------------

    string urlPageParts1 = "https://www.ilcats.ru/toyota/?function=getParts&market=EU&model=671440&modification=LN51L-KRA&complectation=001&group=3&subgroup=5151";
    string urlPageParts2 = "https://www.ilcats.ru/toyota/?function=getParts&market=EU&model=671440&modification=LN51L-KRA&complectation=001&group=3&subgroup=5854";

    ScraperParts scraperParts = new(urlPageParts1);
    var parts = scraperParts.GetScrapingData();

    foreach (var p in parts)
    {
        Console.WriteLine($"Code: {p.PartTreeCode}");
        foreach (var detailParts in p.SubParts)
        {
            Console.WriteLine($"Code: {detailParts.PartCode} Count: {detailParts.Count} Info: {detailParts.Info}");
        }
    }

    Console.WriteLine();
}
