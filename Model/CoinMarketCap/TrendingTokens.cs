using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CoinMarketCap
{
     public class TrendingTokens
    {
        public List<Datum> data { get; set; }
        public Status status { get; set; }
    }
    public class Datum
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
        public int cmc_rank { get; set; }
        public int rank { get; set; }
    }

  
}
