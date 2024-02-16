namespace MVC.Models
{
    public class FilterGetMyTrades
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchTerm { get; set; }
    }
}
