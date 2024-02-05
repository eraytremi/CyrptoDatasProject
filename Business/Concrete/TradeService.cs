using Business.Abstract;
using DataAccess;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Model;
using Model.Dtos;
using Model.Dtos.TradeDto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TradeService : ITradeService
    {
        private readonly ITickerResultService? _tickerResult;
        private readonly CyrptoContext _context;
        Trade trade = new();
        CoinList coinListTable = new();
        public TradeService(ITickerResultService? tickerResult, CyrptoContext context)
        {
            _tickerResult = tickerResult;
            _context = context;
        }

        public async Task LimitBuy(PostTrade dto, long currentUserId)
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
                        throw new Exception("Bakiye yetersiz");
                    }

                    var isMatched = false;

                    while (!isMatched)
                    {
                        var currentDatas = await GetCurrentDatas();
                        var currentData = currentDatas.FirstOrDefault(cd => cd.Symbol == dto.Symbol);

                        if (currentData != null && currentData.LastPrice == dto.Price)
                        {
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
                            trade.Count = dto.Count;
                            trade.Symbol = symbol;
                            trade.WaitingTrades = false;
                            _context.Trades.Add(trade);
                            await _context.SaveChangesAsync();

                            //coinlist tablo güncelle
                            var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol);
                            if (coinList != null)
                            {

                                if (coinList.UserId == currentUserId)
                                {
                                    coinList.Count += dto.Count;
                                    _context.CoinList.Update(coinList);
                                    await _context.SaveChangesAsync();
                                }
                            }

                            else
                            {
                                coinListTable.UserId = currentUserId;
                                coinListTable.Count = dto.Count;
                                coinListTable.Symbol = dto.Symbol;
                                _context.CoinList.Add(coinListTable);
                                await _context.SaveChangesAsync();
                            }
                            var isPriceMatched = true;
                            break; // Döngüden çık


                            if (currentData != null && currentData.LastPrice != dto.Price)
                            {
                                //yapılan işlemi trade tablosuna kaydet
                                trade.UserId = currentUserId;
                                trade.Time = DateTime.Now;
                                trade.isBuy = true;
                                trade.Count = dto.Count;
                                trade.Symbol = symbol;
                                trade.WaitingTrades = true;
                                _context.Trades.Add(trade);
                                await _context.SaveChangesAsync();
                                await Task.Delay(1000);
                            }
                            if (currentData == null)
                            {
                                throw new Exception("Coin listede bulunamadı");
                            }
                        }

                    }


                }

            }
        }

        public async Task LimitSell(PostTrade dto, long currentUserId)
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
                        throw new Exception("Bakiye yetersiz");
                    }

                    var isMatched = false;

                    while (!isMatched)
                    {
                        var currentDatas = await GetCurrentDatas();
                        var currentData = currentDatas.FirstOrDefault(cd => cd.Symbol == dto.Symbol);

                        if (currentData != null && currentData.LastPrice == dto.Price)
                        {
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
                            trade.Count = dto.Count;
                            trade.Symbol = symbol;
                            trade.WaitingTrades = false;
                            _context.Trades.Add(trade);
                            await _context.SaveChangesAsync();

                            //coinlist tablo güncelle
                            var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol && p.UserId==currentUserId);

                            //coinlistte veri varsa düşsün.
                            if (coinList != null )
                            {
                                if (coinList.Count < dto.Count)
                                {
                                    throw new Exception("Yeterli sayıda coin yok!!");
                                }
                            
                                coinList.Count -= dto.Count;
                                _context.CoinList.Update(coinList);
                                await _context.SaveChangesAsync();

                            }
                            var isPriceMatched = true;
                            break; // Döngüden çık
 
                        }

                    }


                }

            }
        }
        public async Task<ApiResponse<NoData>> MarketBuy(PostTrade dto, long currentUserId)
        {
            var getUser = _context.Users.SingleOrDefault(p => p.Id == currentUserId);


            //contextten bakiyeleri getir
            var getList = _context.Bakiye.ToList();

            //paratipi tablosunu bakiyeye include et.
            var includeListParaTipi = _context.Bakiye
                     .Include(b => b.ParaTipi) 
                     .Where(P=>P.UserId==currentUserId)
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
                            trade.Count = dto.Count;
                            trade.Symbol = symbol;
                            trade.Price = convert;
                            _context.Trades.Add(trade);
                            await _context.SaveChangesAsync();
                            

                        //coinlist tablo güncelle 
                        var coinList = _context.CoinList.FirstOrDefault(p => p.Symbol == dto.Symbol && p.UserId == currentUserId);
                        CoinList coinListTable = new();

                        if (coinList!=null)
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
                        
                        if (coinList.Count<dto.Count)
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

        private async Task<List<TickerResult>> GetCurrentDatas()
        {
            var datas = await _tickerResult.GetDatas();
            return datas;
        }
       
    }
}
