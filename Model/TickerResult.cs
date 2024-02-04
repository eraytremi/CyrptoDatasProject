using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TickerResult
    {
        [JsonProperty("e")]
        public string EventType{ get; set; } // Event type
        [JsonProperty("E")]
        [Key]
        public long EventTime { get; set; } // Event time
        [JsonProperty("s")]
        public string Symbol { get; set; } // Symbol
        [JsonProperty("p")]
        public string PriceChange { get; set; } // Price change
        [JsonProperty("P")]
        public string PriceChangePercent { get; set; } // Price change percent
        [JsonProperty("w")]
        public string WeightedAveragePrice { get; set; } // Weighted average price
        [JsonProperty("x")]
        public string PreviousDayClosePrice { get; set; } // Previous day's close price
        [JsonProperty("c")]
        public string LastPrice { get; set; } // Last price
        [JsonProperty("Q")]
        public string LastQuantity { get; set; } // Last quantity
        [JsonProperty("b")]
        public string BestBidPrice { get; set; } // Best bid price
        [JsonProperty("B")]
        public string BestBidQuantity { get; set; } // Best bid quantity,
        [JsonProperty("a")]
        public string BestAskPrice { get; set; } // Best ask price
        [JsonProperty("A")]
        public string BestAskQuantity { get; set; } // Best ask quantity
        [JsonProperty("o")]
        public string OpenPrice { get; set; } // Open price
        [JsonProperty("h")]
        public string HighPrice{ get; set; } // High price
        [JsonProperty("l")]
        public string LowPrice{ get; set; } // Low price
        [JsonProperty("v")]
        public string TotalTradedBaseAssetVolume { get; set; } // Total traded base asset volume
        [JsonProperty("q")]
        public string TotalTradedQuoteAssetVolume { get; set; } // Total traded quote asset volume
        [JsonProperty("O")]
        public long StatisticsOpenTime { get; set; } // Statistics open time
        [JsonProperty("C")]
        public long StatisticsCloseTime { get; set; } // Statistics close time
        [JsonProperty("F")]
        public long FirstTradeID { get; set; } // First trade ID
        [JsonProperty("L")]
        public long LastTradeID { get; set; } // Last trade ID
        [JsonProperty("n")]
        public int TotalNumberOfTrades { get; set; } // Total number of trades
    }
}
