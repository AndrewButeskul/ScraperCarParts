using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    internal class SubGroup
    {
        public int SubGroupId { get; set; }
        public string SubGroupName { get; set; }
        ICollection<Parts> Parts { get; set; }

        public int GroupId { get; set; }
        public GroupOfParts GroupOfPart { get; set; }
    }
}
