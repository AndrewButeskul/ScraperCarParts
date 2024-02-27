using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class GroupOfParts
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public ICollection<SubGroup>? SubGroups { get; set; }
        public string Url { get; set; }

        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }
    }
}
