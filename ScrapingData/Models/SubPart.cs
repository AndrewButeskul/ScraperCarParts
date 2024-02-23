using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class SubPart
    {
        public int SubPartId { get; set; }
        public string PartCode { get; set; }
        public string Count { get; set; }
        public string DateRange { get; set; }
        public string Info {  get; set; }

        public int PartId { get; set; }
        public Part Part { get; set; }
    }
}
