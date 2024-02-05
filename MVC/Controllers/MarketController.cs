using Humanizer;
using Microsoft.AspNetCore.Mvc;
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
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> BuyMarket(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/buymarket", JsonSerializer.Serialize(post), token:token.Token);
          
            return View("/Market/Index");

        }


        [HttpPost]
        public async Task<IActionResult> SellMarket(PostMarketItem post)
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<ApiResponse<PostMarketItem>>("/Trade/sellmarket", JsonSerializer.Serialize(post), token: token.Token);
            if (response != null)
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
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<PostMarketItem>("/Trade/buylimit", JsonSerializer.Serialize(post), token: token.Token);
            if (response != null)
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
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _httpApiService.PostDataAsync<PostMarketItem>("/Trade/selllimit", JsonSerializer.Serialize(post), token: token.Token);
            if (response != null)
            {
                return Json(new { IsSuccess = true, Message = "Market satış işlemi başarıyla gerçekleşti", response });

            }
            else
            {
                return Json(new { IsSuccess = false });
            }

        }
    }
}
