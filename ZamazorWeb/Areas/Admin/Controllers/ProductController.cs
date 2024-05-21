using Microsoft.AspNetCore.Mvc;
using Zamazor.DataAccess.Repos.IRepository;
using Zamazor.Models;
using ZamazorWeb.DataAccess.Data;

namespace ZamazorWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        //Implementation of ApplicatonDbContext
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //Retrieve categories from sql server and pass object to view
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
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
        public IActionResult Create(Product obj)
        {
            //server side validation
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
                //ModelState.AddModelError("name","The Display Order cannot exactyl match the Name.");
            //}

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        //implementation of Edit button
        //GET
        public IActionResult Edit(int? id) //we need id user wants to edit
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Product? ProductFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product edited successfully";
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
            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Product? ProductFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
