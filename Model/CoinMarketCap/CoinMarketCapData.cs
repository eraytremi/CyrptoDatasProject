using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Model.CoinMarketCap
{
    public class CoinMarketCapData
    {
        public string Name { get; set; }
        public string Symbol { get; set; }

        [JsonProperty("cmc_rank")]
        public long? CmcRank { get; set; }

        [JsonProperty("circulating_supply")]
        public decimal CirculatingSupply { get; set; }

        [JsonProperty("total_supply")]
        public decimal? TotalSupply { get; set; }

        [JsonProperty("max_supply")]
        public decimal? MaxSupply { get; set; }

        [JsonProperty("infinite_supply")]
        public bool? InfiniteSupply { get; set; }

        [JsonProperty("last_updated")]
        public DateTime? LastUpdated { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }

    }
}
