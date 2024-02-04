using Newtonsoft.Json;

namespace MVC.Models.Datas
{
    public class TickerResult
    {
        [JsonProperty("E")]
        public long eventTime { get; set; } // Event time
        [JsonProperty("s")]
        public string symbol { get; set; } // Symbol
        [JsonProperty("p")] 
        public string priceChange { get; set; } // Price change
        [JsonProperty("P")]
        public string priceChangePercent { get; set; } // Price change percent
        [JsonProperty("c")]
        public string lastPrice { get; set; } // Last price
    }
}
