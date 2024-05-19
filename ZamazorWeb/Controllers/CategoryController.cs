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
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //Adding new row in database
        //POST
        [HttpPost]
		public IActionResult Create(Category obj)
		{
			//server side validation
			if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("name","The Display Order cannot exactyl match the Name.");
            }

			if (ModelState.IsValid)
            {
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"]= "Category created successfully";
				return RedirectToAction("Index");
			}
            return View();
		}
		//implementation of Edit button
		//GET
		public IActionResult Edit(int? id) //we need id user wants to edit
		{
			if (id== null || id == 0) 
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(id);
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
			//Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
			
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		//POST
		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category edited successfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		//implementation of Delete button
		//GET
		public IActionResult Delete(int? id) //we need id user wants to edit
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category? categoryFromDb = _db.Categories.Find(id);
			//Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
			//Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		//POST
		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category? obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
