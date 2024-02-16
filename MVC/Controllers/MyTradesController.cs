using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.User;
using MVC.Models.ViewModels;
using PurchasingSystem.Web.ApiServices.Interfaces;
using PurchasingSystem.Web.Extensions;
using System.Text.Json;

namespace MVC.Controllers
{
    public class MyTradesController : Controller
    {
        private readonly IHttpApiService _service;

        public MyTradesController(IHttpApiService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(FilterGetMyTrades dto)
        { 
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _service.PostDataAsync<PaginationList<MyTradesItem>>("/user/getmytrades",token.Data.Token,JsonSerializer.Serialize(dto));
            return View(response);
        }

       
    }
}
