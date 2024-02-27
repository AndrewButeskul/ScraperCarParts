using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public static class Logger
    {
        public static void LogToConsole<T>(IScraperData<T> scraper)
        {
            Console.WriteLine($"Info: {scraper.GetScraperInfo()}");
        }
    }
}
