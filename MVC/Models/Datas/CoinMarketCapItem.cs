using Newtonsoft.Json;

namespace MVC.Models.Datas
{
    public class CoinMarketCapItem
    {
        public List<CryptoCurrency> Data { get; set; }

        public Status Status { get; set; }
    }

    public class CryptoCurrency
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public string Slug { get; set; }

        public int CmcRank { get; set; }

        public int NumMarketPairs { get; set; }

        public decimal CirculatingSupply { get; set; }

        public decimal TotalSupply { get; set; }

        public decimal? MaxSupply { get; set; }

        public bool InfiniteSupply { get; set; }

        public DateTime LastUpdated { get; set; }

        public DateTime DateAdded { get; set; }

        public List<string> Tags { get; set; }

        public object Platform { get; set; } // Bu alanın tipi API belgelerine göre değişebilir

        public decimal? SelfReportedCirculatingSupply { get; set; }

        public decimal? SelfReportedMarketCap { get; set; }

        public Dictionary<string, Quote> Quote { get; set; }
    }

    public class Quote
    {
        public decimal Price { get; set; }

        public decimal Volume24H { get; set; }

        public decimal VolumeChange24H { get; set; }

        public decimal PercentChange1H { get; set; }

        public decimal PercentChange24H { get; set; }

        public decimal PercentChange7D { get; set; }

        public decimal MarketCap { get; set; }

        public decimal MarketCapDominance { get; set; }

        public decimal FullyDilutedMarketCap { get; set; }

        public DateTime LastUpdated { get; set; }
    }

    public class Status
    {
        public DateTime Timestamp { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public int Elapsed { get; set; }

        public int CreditCount { get; set; }
    }

}
