using HtmlAgilityPack;
using ScrapingData;
using ScrapingData.Configurations;
using ScrapingData.Models;
using ScrapingData.Scrapers;
using System.Globalization;

//SimpleTestScraping();

string URL = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";

ScraperManager scraperManager = new(URL);
var data = scraperManager.ScrapingData();

Console.WriteLine("ScrapingManager has finished its process successfully.");

CreateDb();
AddDataToDb(data);

Console.WriteLine("All data have been saved to Db successfully.");

static void AddDataToDb(IEnumerable<ModelName> data)
{
    using var dbContext = new AppDbContext();

    dbContext.AddRange(data);
    dbContext.SaveChanges();
}
static void CreateDb()
{
    using var dbContext = new AppDbContext();

    dbContext.Database.EnsureDeleted(); // if db exists command will remove it 
    dbContext.Database.EnsureCreated();
}

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
        foreach (var model in modelName.SubModels)
        {
            Console.WriteLine($"{model.ModelName} {model.SpecificationName} {model.DateRange}");
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
