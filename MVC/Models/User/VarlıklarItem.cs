namespace MVC.Models.User
{
    public class VarlıklarItem
    {
        public long UserId { get; set; }
        public List<BakiyeDetay> Bakiyeler { get; set; }
        public List<CoinListDetay> Coinler { get; set; }
    }
    public class BakiyeDetay
    {
        public string DovizTipi { get; set; }
        public decimal ParaMiktari { get; set; }
    }

    public class CoinListDetay
    {
        public string Symbol { get; set; }
        public double Count { get; set; }
    }
}
