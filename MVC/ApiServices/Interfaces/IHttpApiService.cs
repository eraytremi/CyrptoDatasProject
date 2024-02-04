namespace PurchasingSystem.Web.ApiServices.Interfaces
{
    public interface IHttpApiService
    {
        Task<T> GetDataAsync<T>(string endPoint, string token = null);
        Task<T> PostDataAsync<T>(string endPoint, string jsonData, string token = null);
        Task<T> DeleteDataAsync<T>(string endPoint, string token = null);
        Task<T> PutDataAsync<T>(string endPoint, string jsonData, string token = null);
    }
}
