using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.CoinMarketCap
{
    public class CoinMarketCapExchangeData
    {
        public double Price { get; set; }

        [JsonPropertyName("volume_24h")]
        public decimal Volume24h { get; set; }

        [JsonPropertyName("volume_change_24h")]
        public double VolumeChange24h { get; set; }

        [JsonPropertyName("percent_change_1h")]
        public double PercentChange1h { get; set; }

        [JsonPropertyName("percent_change_24h")]
        public double PercentChange24h { get; set; }

        [JsonPropertyName("percent_change_7d")]
        public double PercentChange7d { get; set; }

        [JsonPropertyName("percent_change_30d")]
        public double PercentChange30d { get; set; }

        [JsonPropertyName("percent_change_60d")]
        public double PercentChange60d { get; set; }

        [JsonPropertyName("percent_change_90d")]
        public double PercentChange90d { get; set; }

        [JsonPropertyName("market_cap")]
        public double MarketCap { get; set; }

        [JsonPropertyName("fully_diluted_market_cap")]
        public double FullyDilutedMarketCap { get; set; }

        [JsonPropertyName("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}
