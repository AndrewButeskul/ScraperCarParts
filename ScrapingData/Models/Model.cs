﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    internal class Model
    {
        public int ModelId { get; set; }
        public string ModelCode { get; set; }
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public string DateRange { get; set; }
        public string SpecificationName { get; set; }
        public ICollection<Equipment> Equipments { get; set; }

        public int ModelNameId { get; set; }
        public ModelName Name { get; set; }
    }
}