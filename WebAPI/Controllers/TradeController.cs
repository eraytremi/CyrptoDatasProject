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
        private readonly IPastDatasService _pastDatasService;
        public TradeController(ITradeService tradeService, IPastDatasService pastDatasService)
        {
            _tradeService = tradeService;
            _pastDatasService = pastDatasService;
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

        [HttpGet("coinmarketNews")]
        public async Task<IActionResult> CoinMarketGetNews()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.CoinMarketCapGetNews(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }

        [HttpGet("coinmarketTrendingTokens")]
        public async Task<IActionResult> CoinMarketGetTrendingTokens()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response = await _tradeService.CoinMarketCapGetTrendingTokens(currentUserId.GetValueOrDefault());
            return SendResponse(response);

        }


        [HttpGet("pastDatas")]
        public async Task<IActionResult> GetPastDatas()
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            await _pastDatasService.InsertPastDatasFromCryptoCompare(currentUserId.GetValueOrDefault());
            return Ok();

        }


        [HttpPost("calculateValue")]
        public async Task<IActionResult> CalculatePastAndCurrentValue([FromQuery]DateTime date, string symbol,double bid)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            var response=await _pastDatasService.GetCalculatePastAndCurrentValue(date,symbol,bid);
            return SendResponse(response);

        }
    }
}
