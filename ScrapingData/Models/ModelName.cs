using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapingData.Models
{
    internal class ModelName
    {
        public int ModelNameId { get; set; }
        public string Name { get; set; }
        ICollection<Model> Models { get; set; }
        
    }
}
