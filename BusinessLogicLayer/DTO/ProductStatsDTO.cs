using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ProductStatsDTO
    {
        public int ProductCount { get; set; }
        public decimal ProductsAveragePrice { get; set; }
        public decimal ProductMaximumPrice { get; set; }
        public decimal ProductMinimumPrice { get; set; }
    }
}
