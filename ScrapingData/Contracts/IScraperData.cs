﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Contracts
{
    public interface IScraperData<T>
    {
        List<T> GetScrapingData();
        string GetScraperInfo();
    }
}
