using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class Part
    {
        public int PartId { get; set; }
        //public string PartCode { get; set; }
        public string PartTreeCode { get; set; }
        //public byte Count { get; set; }
        public string NameTree { get; set; }
        //public string DateRange { get; set; }
        //public string Info { get; set; }
        public ICollection<SubPart> SubParts { get; set; }

        public int SubGroupId { get; set; }
        public SubGroup SubGroup { get; set; }
    }
}
