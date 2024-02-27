using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    public class ModelName
    {
        public int ModelNameId { get; set; }
        public string Name { get; set; }
        public ICollection<SubModel>? SubModels { get; set; }
        
    }
}
