using Azure;
using Business.Abstract;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TradeDto;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : BaseController
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpPost("buymarket")]
        public async Task<IActionResult> BuyMarket(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response= await _tradeService.MarketBuy(dto,currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpPost("sellmarket")]
        public async Task<IActionResult> SellMarket(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response= await _tradeService.MarketSell(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpPost("buylimit")]
        public async Task<IActionResult> BuyLimit(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.LimitBuy(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpPost("selllimit")]
        public async Task<IActionResult> SellLimit(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.LimitSell(dto, currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpGet("ortalamaMaliyet")]
        public async Task<IActionResult> OrtalamaMaliyet()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.OrtalamaMaliyet(currentUserId.GetValueOrDefault());
            return SendResponse(response);
        }

        [HttpGet("coinmarketdata")]
        public async Task<IActionResult> CoinMarketData()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.CoinMarketCap(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }
    }
}
