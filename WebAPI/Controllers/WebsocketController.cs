using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsocketController : ControllerBase
    {
        private readonly ITickerResultService _tickerResultService;

        public WebsocketController(ITickerResultService tickerResultService)
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
