using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class TickerData
    {
        [JsonProperty("e")]
        public string EventType { get; set; } // e

        [JsonProperty("E")]
        public long EventTime { get; set; }   // E

        [JsonProperty("s")]
        public string Symbol { get; set; }    // s

        [JsonProperty("p")]
        public decimal PriceChange { get; set; } // p

        [JsonProperty("P")]
        public decimal PriceChangePercent { get; set; } // P

        [JsonProperty("w")]
        public decimal WeightedAveragePrice { get; set; } // w

        [JsonProperty("x")]
        public decimal PreviousClosePrice { get; set; }  // x

        [JsonProperty("c")]
        public decimal LastPrice { get; set; }   // c

        [JsonProperty("Q")]
        public decimal LastQuantity { get; set; }  // Q

        [JsonProperty("b")]
        public decimal BestBidPrice { get; set; }  // b

        [JsonProperty("B")]
        public decimal BestBidQuantity { get; set; }  // B

        [JsonProperty("a")]
        public decimal BestAskPrice { get; set; }   // a

        [JsonProperty("A")]
        public decimal BestAskQuantity { get; set; }  // A

        [JsonProperty("o")]
        public decimal OpenPrice { get; set; }   // o

        [JsonProperty("h")]
        public decimal HighPrice { get; set; }   // h

        [JsonProperty("l")]
        public decimal LowPrice { get; set; }    // l

        [JsonProperty("v")]
        public decimal TotalTradedBaseAssetVolume { get; set; }  // v

        [JsonProperty("q")]
        public decimal TotalTradedQuoteAssetVolume { get; set; } // q

        [JsonProperty("O")]
        public long StatisticsOpenTime { get; set; }  // O

        [JsonProperty("C")]
        public long StatisticsCloseTime { get; set; } // C

        [JsonProperty("F")]
        public long FirstTradeId { get; set; }       // F

        [JsonProperty("L")]
        public long LastTradeId { get; set; }        // L

        [JsonProperty("n")]
        public long TotalNumberOfTrades { get; set; } // n
    }
}
