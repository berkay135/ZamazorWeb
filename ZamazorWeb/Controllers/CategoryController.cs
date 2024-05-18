using Microsoft.AspNetCore.Mvc;

namespace ZamazorWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
