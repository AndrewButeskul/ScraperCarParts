﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    public interface IDataBuilder
    {
        Data Build();
        //Data GetData();
    }
}