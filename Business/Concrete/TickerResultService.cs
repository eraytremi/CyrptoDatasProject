using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TickerResultService : ITickerResultService
    {

        private readonly IWebSocketBinance _webSocket;
        public TickerResultService(IWebSocketBinance webSocket)
        {
            _webSocket = webSocket;
            var serverUri = new Uri("wss://stream.binance.com:9443/ws/!ticker@arr");
            _webSocket.ConnectAsync(serverUri).Wait();
        }
        public async Task<List<TickerResult>> GetDatas()
        {
            var tickerStats = await _webSocket.ReceiveAsync<List<TickerResult>>();
            return tickerStats;
        }
       
    }
}
