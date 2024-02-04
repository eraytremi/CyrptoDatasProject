using Microsoft.AspNetCore.Mvc;
using MVC.Models.User;
using PurchasingSystem.Web.ApiServices.Interfaces;
using PurchasingSystem.Web.Extensions;

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
        public async Task<IActionResult> Index()
        { 

            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _service.GetDataAsync<List<MyTradesItem>>("/user/getmytrades",token.Token);
            return View(response);
        }
    }
}
