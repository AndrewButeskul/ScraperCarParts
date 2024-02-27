using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class SubGroup
    {
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        public ICollection<Part>? Parts { get; set; }
        public string Url { get; set; }

        public int GroupId { get; set; }
        public GroupOfPart? GroupOfPart { get; set; }
    }
}
