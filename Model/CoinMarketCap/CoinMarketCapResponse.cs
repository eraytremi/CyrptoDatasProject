using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class CoinMarketCapResponse
{
    [JsonProperty("data")]
    public List<CryptoCurrency> Data { get; set; }

    [JsonProperty("status")]
    public Status Status { get; set; }
}

public class CryptoCurrency
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("slug")]
    public string Slug { get; set; }

    [JsonProperty("cmc_rank")]
    public int CmcRank { get; set; }

    [JsonProperty("num_market_pairs")]
    public int NumMarketPairs { get; set; }

    [JsonProperty("circulating_supply")]
    public decimal CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal TotalSupply { get; set; }

    [JsonProperty("max_supply")]
    public decimal? MaxSupply { get; set; }

    [JsonProperty("infinite_supply")]
    public bool InfiniteSupply { get; set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; }

    [JsonProperty("date_added")]
    public DateTime DateAdded { get; set; }

    [JsonProperty("tags")]
    public List<string> Tags { get; set; }

    [JsonProperty("platform")]
    public object Platform { get; set; } // Bu alanın tipi API belgelerine göre değişebilir

    [JsonProperty("self_reported_circulating_supply")]
    public decimal? SelfReportedCirculatingSupply { get; set; }

    [JsonProperty("self_reported_market_cap")]
    public decimal? SelfReportedMarketCap { get; set; }

    [JsonProperty("quote")]
    public Dictionary<string, Quote> Quote { get; set; }
}

public class Quote
{
    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("volume_24h")]
    public decimal Volume24H { get; set; }

    [JsonProperty("volume_change_24h")]
    public decimal VolumeChange24H { get; set; }

    [JsonProperty("percent_change_1h")]
    public decimal PercentChange1H { get; set; }

    [JsonProperty("percent_change_24h")]
    public decimal PercentChange24H { get; set; }

    [JsonProperty("percent_change_7d")]
    public decimal PercentChange7D { get; set; }

    [JsonProperty("market_cap")]
    public decimal MarketCap { get; set; }

    [JsonProperty("market_cap_dominance")]
    public decimal MarketCapDominance { get; set; }

    [JsonProperty("fully_diluted_market_cap")]
    public decimal FullyDilutedMarketCap { get; set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; set; }
}

public class Status
{
    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("error_code")]
    public int ErrorCode { get; set; }

    [JsonProperty("error_message")]
    public string ErrorMessage { get; set; }

    [JsonProperty("elapsed")]
    public int Elapsed { get; set; }

    [JsonProperty("credit_count")]
    public int CreditCount { get; set; }
}
