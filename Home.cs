using Microsoft.AspNetCore.Mvc;

namespace projectnew.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
