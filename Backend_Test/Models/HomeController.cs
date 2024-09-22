using Microsoft.AspNetCore.Mvc;

namespace Backend_Test.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
