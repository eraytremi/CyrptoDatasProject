using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.Models;
using MVC.Models.Datas;
using MVC.Models.User;
using PurchasingSystem.Web.ApiServices.Interfaces;
using PurchasingSystem.Web.Extensions;
using System.Text.Json;

namespace MVC.Controllers
{
    public class MarketController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public MarketController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> BuyMarket(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/buymarket", JsonSerializer.Serialize(post), token:token.Data.Token);
                
            if (response.StatusCode == 200)
            {   
                return Json(new { IsSuccess = true, Message = "Market satış işlemi başarıyla gerçekleşti" ,response});

            }
            else
            {
                return Json(new {IsSuccess=true,response.ErrorMessages });
            }

        }


        [HttpPost]
        public async Task<IActionResult> SellMarket(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/sellmarket", JsonSerializer.Serialize(post), token: token.Data.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Market satış işlemi başarıyla gerçekleşti", response });

            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }
        [HttpPost]
        public async Task<IActionResult> BuyLimit(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/buylimit", JsonSerializer.Serialize(post), token: token.Data.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Market satış işlemi başarıyla gerçekleşti", response });

            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SellLimit(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/selllimit", JsonSerializer.Serialize(post), token: token.Data.Token);
            if (response.StatusCode == 200)
            {
                return Json(new { IsSuccess = true, Message = "Market satış işlemi başarıyla gerçekleşti", response });

            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }

        public async Task<IActionResult> CoinMarketData()
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _httpApiService.GetDataAsync<ApiResponse<CoinMarketCapItem>>("/Trade/coinmarketdata", token: token.Data.Token);
            return View(response.Data);
        }
    }
}
