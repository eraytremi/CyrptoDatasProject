using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos.TradeDto
{
    public class PostTrade
    {
        public string Symbol { get; set; }
        public double Count { get; set; }
        public string Price { get; set; }
    }
}
