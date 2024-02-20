using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData
{
    internal class Model
    {
        public int ModelCode { get; set; }
        public string ModelName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SpecificationName { get; set; }
    }
}
