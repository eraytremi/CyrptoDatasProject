using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Models.User;
using MVC.Models.ViewModels;
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
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _service.GetDataAsync<ApiResponse<VarlıklarItem>>("/user/varliklarim", token.Data.Token);

            var averageCost = await GetAverageCosts();

            VarlıklarimVM varlıklarimVM = new()
            {
                Varliklar = response.Data,
                AverageCosts = averageCost.Data
            };
            return View(varlıklarimVM);
        }

        private async Task<ApiResponse<AverageCost>> GetAverageCosts()
        {
            var token = HttpContext.Session.GetObject<ApiResponse<UserGetDto>>("ActivePerson");
            var response = await _service.GetDataAsync<ApiResponse<Dictionary<string,decimal>>>("/Trade/ortalamaMaliyet", token.Data.Token);

            var averageCost = new AverageCost { Cost = response.Data };
            return new ApiResponse<AverageCost> { Data=averageCost};
        }


    }
}
