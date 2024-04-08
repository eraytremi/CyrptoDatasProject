using Business.Abstract;
using DataAccess;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model.PastDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PastDatasService : IPastDatasService
    {
        private readonly HttpClient _httpClient;
        private readonly CyrptoContext _cyrptoContext;
        public PastDatasService(HttpClient httpClient, CyrptoContext cyrptoContext)
        {
            _httpClient = httpClient;
            _cyrptoContext = cyrptoContext;
        }

        public async Task InsertPastDatasFromCryptoCompare(long currentUserId)
        {
            string[] symbols = { "BTC", "ETH"};
            string[] parities = { "USDT" };
            foreach (string symbol in symbols)
            {
                foreach (var parity in parities)
                {
                    string urlPastData = $"https://min-api.cryptocompare.com/data/histoday?fsym={symbol}&tsym={parity}&limit=2000";
                    HttpResponseMessage responseMessage = await _httpClient.GetAsync(urlPastData);
                    responseMessage.EnsureSuccessStatusCode();
                    string responseData = await responseMessage.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<PastDatasRequest>(responseData);
                    foreach (var item in data.Data)
                    {

                        var newData = new GetPastDatas()
                        {
                            Price = item.close,
                            Symbol = $"{symbol}{parity}",
                            Time = UnixTimeStampToDateTime(item.time),
                            VolumeByCurrencyCount = item.volumefrom,
                            VolumeByParity = item.volumeto
                        };

                        _cyrptoContext.Add(newData);
                        _cyrptoContext.SaveChanges();

                    }
                }        
            }
          
        }

        public async  Task<ApiResponse<CalculatePastAndCurrentValue>> GetCalculatePastAndCurrentValue(DateTimeOffset date,string symbol,double bid)
        {
            var getDataByTimeAndSymbol = await _cyrptoContext.PastDatas.SingleOrDefaultAsync(x => x.Time.Date == date.Date && x.Symbol == symbol);
            var getCurrentData = await _cyrptoContext.PastDatas.SingleOrDefaultAsync(x=>x.Time.Date ==DateTime.Now.Date && x.Symbol==symbol);

            var howManyCoinCountInThePast = bid / getDataByTimeAndSymbol.Price;

            var currentValue = howManyCoinCountInThePast * getCurrentData.Price;

            var data = new CalculatePastAndCurrentValue()
            {
                Count = howManyCoinCountInThePast,
                Price = currentValue,
            };

            return ApiResponse<CalculatePastAndCurrentValue>.Success(StatusCodes.Status200OK, data);
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
           
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(unixTimeStamp)
                .ToLocalTime(); 

            return dateTime;
        }
    }
}
