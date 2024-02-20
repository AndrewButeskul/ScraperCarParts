using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    internal interface IScraperData<T>
    {
        List<T> GetScrapingData();

    }
}
