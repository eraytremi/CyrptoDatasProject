using Business.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Net.WebSockets;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebSocketBinanceController
    {
       private readonly ITickerResultService _tickerResultService;

        public WebSocketBinanceController(ITickerResultService tickerResultService)
        {
            _tickerResultService = tickerResultService;
            
        }

        [HttpGet("getlistdatas")]
        public async Task<ActionResult<List<TickerResult>>> GetDatas()
        {
            var response = await _tickerResultService.GetDatas();
            return response;
        }
    }
}
