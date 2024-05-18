using Microsoft.AspNetCore.Mvc;
using ZamazorWeb.Data;
using ZamazorWeb.Models;

namespace ZamazorWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        //Implementation of ApplicatonDbContext
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //Retrieve categories from sql server and pass object to view
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        //Implementation of Create button
        public IActionResult Create()
        {
            return View();
        }
    }
}
