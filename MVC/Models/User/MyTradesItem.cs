namespace MVC.Models.User
{
    public class MyTradesItem
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime Time { get; set; }
        public bool isBuy { get; set; }
        public bool isSell { get; set; }
        public double Count { get; set; }
        public string Symbol { get; set; }
    }
}
