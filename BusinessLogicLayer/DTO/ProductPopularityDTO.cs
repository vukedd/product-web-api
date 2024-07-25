using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ProductPopularityDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int NumberOfConnections { get; set; }
        public string OwnerName { get; set; }
    }
}
