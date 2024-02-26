using Business.Abstract;
using DataAccess;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using Model.CoinMarketCap;
using Model.Dtos;
using Model.Dtos.TradeDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
    
namespace Business.Concrete
{
    public class TradeService : ITradeService
    {
        private const string apiKey = "19b6d8ac-e882-4365-8dd7-3289768d6e30";
        private const string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
        private const string urlNews = "https://pro-api.coinmarketcap.com/v1/content/latest";
        private const string urlTrendingTokens = "https://pro-api.coinmarketcap.com/v1/community/trending/token";
        private const string urlHistoricalDatas= "https://api.binance.com/api/v3/aggTrades";

        private readonly ITickerResultService? _tickerResult;
        private readonly CyrptoContext _context;
        Trade trade = new();
        CoinList coinListTable = new();
        public TradeService(ITickerResultService? tickerResult, CyrptoContext context)
        {
            _tickerResult = tickerResult;
            _context = context;
        }

        public async Task<ApiResponse<CoinMarketCapResponse>> CoinMarketCap(long currentUserId)
        {
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);


                //Supply verilerini getir
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CoinMarketCapResponse>(responseBody);

                //Volume verilerini getir 


                return ApiResponse<CoinMarketCapResponse>.Success(StatusCodes.Status200OK, data);
            }


        }


        public async Task<ApiResponse<CoinMarketCapNewsResponse>> CoinMarketCapGetNews(long currentUserId)
        {

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

                var response = await client.GetAsync(urlNews);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<CoinMarketCapNewsResponse>(responseBody);

                return ApiResponse<CoinMarketCapNewsResponse>.Success(StatusCodes.Status200OK, data);
            }


        }


        public async Task<ApiResponse<TrendingTokens>> CoinMarketCapGetTrendingTokens(long currentUserId)
        {

            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);

                var response = await client.GetAsync(urlTrendingTokens);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<TrendingTokens>(responseBody);

                return ApiResponse<TrendingTokens>.Success(StatusCodes.Status200OK, data);
            }


        }

       

        static long ConvertToUnixTimestamp(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        //public async Task<ApiResponse<NoData>> LimitBuy(PostTrade dto, long currentUserId)
        //{
        //    var getUser = _context.Users.SingleOrDefault(p => p.Id == currentUserId);


        //    //contextten bakiyeleri getir
        //    var getList = _context.Bakiye.ToList();

        //    //paratipi tablosunu bakiyeye include et.
        //    var includeListParaTipi = _context.Bakiye
        //             .Include(b => b.ParaTipi)
        //             .Where(P => P.UserId == currentUserId)
        //             .ToList();

        //    //seçtiğin değerin paritesi sende var mı?
        //    string symbol = dto.Symbol;
        //    foreach (var item in includeListParaTipi)
        //    {

        //        var existDöviz = dto.Symbol.Contains(item.ParaTipi.DövizTipi);
        //        if (existDöviz)
        //        {
        //            var convert = decimal.Parse(dto.Price);
        //            decimal convertedValue = convert / 100000000M;


        //            //aynı ise alınacak adet ile o anki fiyatı ile çarp. Maliyet*
        //            var buyTrade = convertedValue * Convert.ToDecimal(dto.Count);
        //            if (buyTrade > item.ParaMiktarı)
        //            {
        //                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bakiye Yetersiz");

        //            }

        //            var isMatched = false;

        //            while (!isMatched)
        //            {
        //                var currentDatas = await GetCurrentDatas();
        //                var currentData = currentDatas.FirstOrDefault(cd => cd.Symbol == dto.Symbol);

        //                if (currentData != null && currentData.LastPrice == dto.Price)
        //                {
        //                    //bakiyeden alınan miktarı düş.
        //                    var result = item.ParaMiktarı - buyTrade;
        //                    item.UserId = currentUserId;
        //                    item.ParaMiktarı = result;
        //                    _context.Bakiye.Update(item);
        //                    await _context.SaveChangesAsync();

        //                    //yapılan işlemi trade tablosuna kaydet
        //                    trade.UserId = currentUserId;
        //                    trade.Time = DateTime.Now;
        //                    trade.isBuy = true;
        //                    trade.Count = dto.Count;
        //                    trade.Symbol = symbol;
        //                    trade.WaitingTrades = false;
        //                    _context.Trades.Add(trade);
        //                    await _context.SaveChangesAsync();

        //                    //coinlist tablo güncelle
        //                    var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol);
        //                    if (coinList != null)
        //                    {

        //                        if (coinList.UserId == currentUserId)
        //                        {
        //                            coinList.Count += dto.Count;
        //                            _context.CoinList.Update(coinList);
        //                            await _context.SaveChangesAsync();
        //                        }
        //                    }

        //                    else
        //                    {
        //                        coinListTable.UserId = currentUserId;
        //                        coinListTable.Count = dto.Count;
        //                        coinListTable.Symbol = dto.Symbol;
        //                        _context.CoinList.Add(coinListTable);
        //                        await _context.SaveChangesAsync();
        //                    }



        //                    if (currentData != null && currentData.LastPrice != dto.Price)
        //                    {
        //                        //yapılan işlemi trade tablosuna kaydet
        //                        trade.UserId = currentUserId;
        //                        trade.Time = DateTime.Now;
        //                        trade.isBuy = true;
        //                        trade.Count = dto.Count;
        //                        trade.Symbol = symbol;
        //                        trade.WaitingTrades = true;
        //                        _context.Trades.Add(trade);
        //                        await _context.SaveChangesAsync();
        //                        await Task.Delay(1000);
        //                    }
        //                    if (currentData == null)
        //                    {
        //                        return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Coin listede yok");
        //                    }

        //                    var isPriceMatched = true;
        //                    break; // Döngüden çık
        //                }

        //            }


        //        }

        //    }
        //    return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        //}

        //public async Task<ApiResponse<NoData>> LimitSell(PostTrade dto, long currentUserId)
        //{
        //    var getUser = _context.Users.SingleOrDefault(p => p.Id == currentUserId);


        //    //contextten bakiyeleri getir
        //    var getList = _context.Bakiye.ToList();

        //    //paratipi tablosunu bakiyeye include et.
        //    var includeListParaTipi = _context.Bakiye
        //             .Include(b => b.ParaTipi)
        //             .Where(P => P.UserId == currentUserId)
        //             .ToList();

        //    //seçtiğin değerin paritesi sende var mı?
        //    string symbol = dto.Symbol;
        //    foreach (var item in includeListParaTipi)
        //    {

        //        var existDöviz = dto.Symbol.Contains(item.ParaTipi.DövizTipi);
        //        if (existDöviz)
        //        {
        //            var convert = decimal.Parse(dto.Price);
        //            decimal convertedValue = convert / 100000000M;


        //            //aynı ise alınacak adet ile o anki fiyatı ile çarp. Maliyet*
        //            var buyTrade = convertedValue * Convert.ToDecimal(dto.Count);
        //            if (buyTrade > item.ParaMiktarı)
        //            {
        //                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bakiye Yetersiz");
        //            }

        //            var isMatched = false;

        //            while (!isMatched)
        //            {
        //                var currentDatas = await GetCurrentDatas();
        //                var currentData = currentDatas.FirstOrDefault(cd => cd.Symbol == dto.Symbol);

        //                if (currentData != null && currentData.LastPrice == dto.Price)
        //                {
        //                    //bakiyeden alınan miktarı düş.
        //                    var result = item.ParaMiktarı - buyTrade;
        //                    item.UserId = currentUserId;
        //                    item.ParaMiktarı = result;
        //                    _context.Bakiye.Update(item);
        //                    await _context.SaveChangesAsync();

        //                    //yapılan işlemi trade tablosuna kaydet
        //                    trade.UserId = currentUserId;
        //                    trade.Time = DateTime.Now;
        //                    trade.isBuy = true;
        //                    trade.Count = dto.Count;
        //                    trade.Symbol = symbol;
        //                    trade.WaitingTrades = false;
        //                    _context.Trades.Add(trade);
        //                    await _context.SaveChangesAsync();

        //                    //coinlist tablo güncelle
        //                    var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol && p.UserId == currentUserId);

        //                    //coinlistte veri varsa düşsün.
        //                    if (coinList != null)
        //                    {
        //                        if (coinList.Count < dto.Count)
        //                        {
        //                            return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yeterli sayıda coin yok");
        //                        }

        //                        coinList.Count -= dto.Count;
        //                        _context.CoinList.Update(coinList);
        //                        await _context.SaveChangesAsync();

        //                    }
        //                    var isPriceMatched = true;
        //                    break; // Döngüden çık

        //                }

        //            }


        //        }

        //    }

        //    return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        //}


        public async Task<ApiResponse<NoData>> MarketBuy(PostTrade dto, long currentUserId)
        {
            var getUser = _context.Users.SingleOrDefault(p => p.Id == currentUserId);


            //contextten bakiyeleri getir
            var getList = _context.Bakiye.ToList();

            //paratipi tablosunu bakiyeye include et.
            var includeListParaTipi = _context.Bakiye
                     .Include(b => b.ParaTipi)
                     .Where(P => P.UserId == currentUserId)
                     .ToList();

            //seçtiğin değerin paritesi sende var mı?
            string symbol = dto.Symbol;
            foreach (var item in includeListParaTipi)
            {


                var existDöviz = dto.Symbol.Contains(item.ParaTipi.DövizTipi);
                if (existDöviz)
                {
                    var convert = decimal.Parse(dto.Price, CultureInfo.InvariantCulture);
                    //decimal convertedValue = convert / 100000000M;


                    //aynı ise alınacak adet ile o anki fiyatı ile çarp. Maliyet*
                    var buyTrade = convert * Convert.ToDecimal(dto.Count);
                    if (buyTrade > item.ParaMiktarı)
                    {
                        return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bakiye Yetersiz");
                    }



                    //bakiyeden alınan miktarı düş.
                    var result = item.ParaMiktarı - buyTrade;
                    item.UserId = currentUserId;
                    item.ParaMiktarı = result;
                    _context.Bakiye.Update(item);
                    await _context.SaveChangesAsync();

                    //yapılan işlemi trade tablosuna kaydet
                    trade.UserId = currentUserId;
                    trade.Time = DateTime.Now;
                    trade.isBuy = true;
                    trade.isSell = false;
                    trade.Count = dto.Count;
                    trade.Symbol = symbol;
                    trade.Price = convert;
                    _context.Trades.Add(trade);
                    await _context.SaveChangesAsync();


                    //coinlist tablo güncelle 
                    var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol && p.UserId == currentUserId);
                    CoinList coinListTable = new();

                    if (coinList != null)
                    {
                        coinList.Count += dto.Count;
                        _context.CoinList.Update(coinList);
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        coinListTable.UserId = currentUserId;
                        coinListTable.Count = dto.Count;
                        coinListTable.Symbol = dto.Symbol;
                        _context.CoinList.Add(coinListTable);
                        await _context.SaveChangesAsync();
                        return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

                    }

                }

            }

            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
        }

        public async Task<ApiResponse<NoData>> MarketSell(PostTrade dto, long currentUserId)
        {
            var getUser = _context.Users.SingleOrDefault(p => p.Id == currentUserId);

            //contextten bakiyeleri getir
            var getList = _context.Bakiye.ToList();

            //paratipi tablosunu bakiyeye include et.
            var includeListParaTipi = _context.Bakiye
                     .Include(b => b.ParaTipi)
                     .Where(P => P.UserId == currentUserId)
                     .ToList();

            //seçtiğin değerin paritesi sende var mı?
            string symbol = dto.Symbol;
            foreach (var item in includeListParaTipi)
            {


                var existDöviz = dto.Symbol.Contains(item.ParaTipi.DövizTipi);
                if (existDöviz)
                {
                    var convert = decimal.Parse(dto.Price);
                    decimal convertedValue = convert / 100000000M;


                    //aynı ise alınacak adet ile o anki fiyatı ile çarp. Maliyet*
                    var buyTrade = convertedValue * Convert.ToDecimal(dto.Count);
                    if (buyTrade > item.ParaMiktarı)
                    {
                        return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bakiye Yetersiz");

                    }

                    //bakiyeden alınan miktarı düş.
                    var result = item.ParaMiktarı + buyTrade;
                    item.UserId = currentUserId;
                    item.ParaMiktarı = result;
                    _context.Bakiye.Update(item);
                    await _context.SaveChangesAsync();

                    //yapılan işlemi trade tablosuna kaydet
                    trade.UserId = currentUserId;
                    trade.Time = DateTime.Now;
                    trade.isSell = true;
                    trade.isBuy = false;
                    trade.Count = dto.Count;
                    trade.Symbol = symbol;
                    trade.Price = convert;
                    _context.Trades.Add(trade);
                    await _context.SaveChangesAsync();

                    //coinlist tablo güncelle
                    var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol && p.UserId == currentUserId);

                    //coinlistte veri varsa düşsün.
                    if (coinList != null)
                    {

                        if (coinList.Count < dto.Count)
                        {
                            return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "yeterli sayıda coin yok!!");

                        }
                        coinList.Count -= dto.Count;
                        _context.CoinList.Update(coinList);
                        await _context.SaveChangesAsync();

                    }
                    //yoksa olmayan malı satamazsın zaten
                    else
                    {
                        return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, $"Varlıklarında {dto.Symbol} yok!");


                    }
                }

            }
            return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

        }

        public async Task<ApiResponse<Dictionary<string, decimal>>> OrtalamaMaliyet(long currentUserId)
        {
            var getUser = await _context.Users.SingleOrDefaultAsync(p => p.Id == currentUserId);
            var getTrades = _context.Trades.Where(t => t.UserId == currentUserId).ToList();

            var groupBy = getTrades.GroupBy(p => p.Symbol).ToList();

            var averageCosts = new Dictionary<string, decimal>();

            foreach (var getTrade in groupBy)
            {
                var symbolKey = getTrade.Key;
                var tradesForSymbols = getTrade.ToList();

                decimal totalCost = 0;
                decimal totalCount = 0;

                foreach (var tradesForSymbol in tradesForSymbols)
                {
                    totalCost += tradesForSymbol.Price * Convert.ToDecimal(tradesForSymbol.Count);
                    totalCount += Convert.ToDecimal(tradesForSymbol.Count);
                }

                decimal averageCost = totalCost / totalCount;
                averageCosts.Add(symbolKey, averageCost);
            }

            return ApiResponse<Dictionary<string, decimal>>.Success(StatusCodes.Status200OK, averageCosts);

        }

       
        //    public Task<ApiResponse<List<TickerResult>>> TickerList(long currentUserId)
        //    {
        //        var websocket = new WebSocketApi("wss://stream.binance.com:9443/ws/!ticker@arr");

        //        websocket.OnMessageReceived(
        //            async (data) =>
        //            {
        //                Console.WriteLine(data);
        //                await Task.CompletedTask;
        //            }, CancellationToken.None);

        //        await websocket.ConnectAsync(CancellationToken.None);

        //        await websocket.AccountTrade.NewOrderAsync(symbol: "BNBUSDT", side: Models.Side.BUY, type: Models.OrderType.LIMIT, timeInForce: Models.TimeInForce.GTC, price: 300, quantity: 1, cancellationToken: CancellationToken.None);

        //        await Task.Delay(5000);
        //        Console.WriteLine("Disconnect with WebSocket API Server");
        //        await websocket.DisconnectAsync(CancellationToken.None);
        //    }

        //    private async Task<List<TickerResult>> GetCurrentDatas()
        //    {

        //        var datas = await _tickerResult.GetDatas();
        //        return datas;
        //    }

        //}
    }
}
