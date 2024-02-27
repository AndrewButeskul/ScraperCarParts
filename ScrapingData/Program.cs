using ScrapingData;
using ScrapingData.Configurations;
using ScrapingData.Models;


string URL = "https://www.ilcats.ru/toyota/?function=getModels&market=EU";

ScraperManager scraperManager = new(URL);
var data = scraperManager.ScrapingData();

Console.WriteLine("** ScrapingManager has finished its process successfully **");

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

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

