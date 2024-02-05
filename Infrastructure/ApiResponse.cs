using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? StatusMessage { get; set; }
        public List<string>? ErrorMessages { get; set; }

        public static ApiResponse<T> Success(int statusCode)
        {
            return new ApiResponse<T> { StatusCode = statusCode, StatusMessage = "İşlem Başarılı" };
        }

        public static ApiResponse<T> Success(int statusCode, T data)
        {
            return new ApiResponse<T> { StatusCode = statusCode, Data = data, StatusMessage = "İşlem Başarılı" };
        }

        public static ApiResponse<T> Fail(int statusCode, string errorMessage)
        {
            return new ApiResponse<T> { StatusCode = statusCode, StatusMessage = "İşlem Başarısız", ErrorMessages = new List<string> { errorMessage } };
        }

        public static ApiResponse<T> Fail(int statusCode, List<string> errorMessages)
        {
            return new ApiResponse<T> { StatusCode = statusCode, StatusMessage = "İşlem Başarısız", ErrorMessages = errorMessages };
        }
    }
}
