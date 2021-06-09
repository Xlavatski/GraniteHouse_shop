using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models.Queries
{
    public class GroupByModelQuery
    {
        public int Count { get; set; }

        public  ProductTypes ProductTypes { get; set; }
    }
}
