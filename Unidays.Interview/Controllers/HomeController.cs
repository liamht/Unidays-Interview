using Microsoft.AspNetCore.Mvc;

namespace Unidays.Interview.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string message = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Message = message;
            }

            return View();
        }
    }
}