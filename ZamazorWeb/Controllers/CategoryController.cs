using Microsoft.AspNetCore.Mvc;
using Zamazor.DataAccess.Repos.IRepository;
using Zamazor.Models;
using ZamazorWeb.DataAccess.Data;

namespace ZamazorWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //Implementation of ApplicatonDbContext
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //Retrieve categories from sql server and pass object to view
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
				_unitOfWork.Category.Add(obj);
				_unitOfWork.Save();
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
			Category? categoryFromDb = _unitOfWork.Category.Get(u=>u.Id==id);
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
				_unitOfWork.Category.Update(obj);
				_unitOfWork.Save();
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
			Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
			Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			_unitOfWork.Category.Remove(obj);
			_unitOfWork.Save();
			TempData["success"] = "Category deleted successfully";
			return RedirectToAction("Index");
		}
	}
}
