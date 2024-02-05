using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos.TradeDto;

namespace WebAPI.Controllers
{
    [Authorize]

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
            await _tradeService.MarketBuy(dto,currentUserId.GetValueOrDefault());
            return Ok();
        }

        [HttpPost("sellmarket")]
        public async Task<IActionResult> SellMarket(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            await _tradeService.MarketSell(dto, currentUserId.GetValueOrDefault());
            return Ok();
        }

        [HttpPost("buylimit")]
        public async Task<IActionResult> BuyLimit(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            await _tradeService.LimitBuy(dto, currentUserId.GetValueOrDefault());
            return Ok();
        }

        [HttpPost("selllimit")]
        public async Task<IActionResult> SellLimit(PostTrade dto)
        {
            var currentUserId = CurrentUser.Get(HttpContext);
            await _tradeService.LimitSell(dto, currentUserId.GetValueOrDefault());
            return Ok();
        }
    }
}
