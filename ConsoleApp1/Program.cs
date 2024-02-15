using DataAccess;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dtos.TradeDto;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

class Program
{
    private readonly CyrptoContext _context;
    Trade trade = new();
    CoinList coinListTable = new();
    public Program(CyrptoContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<NoData>> Buy(PostTrade dto, long currentUserId)
    {
        Uri uri = new Uri("wss://stream.binance.com:9443/ws/!ticker@arr");
            
        using (ClientWebSocket clientWebSocket = new ClientWebSocket())
        {
            try
            {
                await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

                Console.WriteLine("WebSocket'e bağlandı.");

                while (clientWebSocket.State == WebSocketState.Open)
                {
                    byte[] buffer = new byte[1024];
                    WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                     
                    var socketDatas = JsonSerializer.Deserialize<List<TickerResult>>(receivedMessage);
                    
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
                            
                            var currentData = socketDatas.FirstOrDefault(cd => cd.Symbol == dto.Symbol);
                        

                            if (currentData != null && currentData.LastPrice == dto.Price)
                            {
                                //bakiyeden alınan miktarı düş.
                                var result1 = item.ParaMiktarı - buyTrade;
                                item.UserId = currentUserId;
                                item.ParaMiktarı = result1;
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
                                    return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Coin listede yok");
                                }

                            }



                        }

                    }
                    return ApiResponse<NoData>.Success(StatusCodes.Status200OK);

                    var data = JsonSerializer.Deserialize<List<TickerResult>>(receivedMessage);
                    Console.WriteLine($"Alınan mesaj: {receivedMessage}");
                }
                return ApiResponse<NoData>.Success(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return ApiResponse<NoData>.Fail(StatusCodes.Status400BadRequest, "Bir hata oluştu");
            }
        }
    }

  
}
