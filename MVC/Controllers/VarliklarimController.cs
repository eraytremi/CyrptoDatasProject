using Microsoft.AspNetCore.Mvc;
using MVC.Models.User;
using NuGet.Common;
using PurchasingSystem.Web.ApiServices.Interfaces;
using PurchasingSystem.Web.Extensions;

namespace MVC.Controllers
{
    public class VarliklarimController : Controller
    {
        private readonly IHttpApiService _service;

        public VarliklarimController(IHttpApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetObject<UserGetDto>("ActivePerson");
            var response = await _service.GetDataAsync<VarlıklarItem>("/user/varliklarim", token.Token);
            return View(response);
        }


    }
}
