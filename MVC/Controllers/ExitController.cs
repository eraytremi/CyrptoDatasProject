using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ExitController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogIn","Authentication");
        }
    }
}
