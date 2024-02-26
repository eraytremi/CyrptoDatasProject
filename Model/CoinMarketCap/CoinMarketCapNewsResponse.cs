using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CoinMarketCap
{
    public class CoinMarketCapNewsResponse
    {
        public List<Data> data { get; set; }
        public Status status { get; set; }
    }

    public class Asset
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string slug { get; set; }
    }

    public class Data
    {
        public string cover { get; set; }
        public List<Asset> assets { get; set; }
        public DateTime created_at { get; set; }
        public DateTime released_at { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string type { get; set; }
        public string source_name { get; set; }
        public string source_url { get; set; }
    }

    public class Status
    {
        public DateTime timestamp { get; set; }
        public int error_code { get; set; }
        public string error_message { get; set; }
        public int elapsed { get; set; }
        public int credit_count { get; set; }
    }

   
}
