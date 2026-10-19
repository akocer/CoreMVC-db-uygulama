using Microsoft.AspNetCore.Mvc;

namespace uy01.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
