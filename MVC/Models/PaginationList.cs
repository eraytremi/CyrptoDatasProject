namespace MVC.Models
{
    public class PaginationList<T>:List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }   //bir sayfada kaç veri var
        public int TotalCount { get; set; }  //toplam veri
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
