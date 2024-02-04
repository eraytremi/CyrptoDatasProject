using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.User;
using PurchasingSystem.Web.ApiServices.Interfaces;
using PurchasingSystem.Web.Extensions;
using System.Text.Json;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public AuthenticationController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginUserItem dto)
        {
            var response = await _httpApiService.PostDataAsync<UserGetDto>
                 ($"/User", JsonSerializer.Serialize(dto));

            if (response != null)
            {
                HttpContext.Session.SetObject("ActivePerson", response);
                return Redirect("/Market/Index");
            }
            else
            {
                return Json(new { IsSuccess = false, Messages = response });
            }
        }
    }
}
