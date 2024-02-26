using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Pagination
{
    public class PaginatedList<T>:List<T>
    {
       
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }   //bir sayfada kaç veri var
        public int TotalCount { get; set; }  //toplam veri
        public bool HasPrevious => CurrentPage > 1;         
        public bool HasNext =>CurrentPage < TotalPages;

        public PaginatedList(List<T> items,int count,int pageNumber,int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)(Math.Ceiling((double)count / pageSize));
            TotalCount = count;
            PageSize = pageSize;
            AddRange(items);
        }
            
        public static async Task<PaginatedList<T>> ToPagedList(IQueryable<T> source,int pageNumber,int pageSize)
        {   
            var count = await  source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
