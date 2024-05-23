using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zamazor.DataAccess.Repos.IRepository;
using Zamazor.Models;
using Zamazor.Models.ViewModels;
using ZamazorWeb.DataAccess.Data;

namespace ZamazorWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //Implementation of ApplicatonDbContext
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //Retrieve products from sql server and pass object to view
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            
			return View(objProductList);
        }
        

        //Implementation of Create button
        //GET
        public IActionResult Upsert(int? id) //create and edit combined
        {
			//projections(converting category to select list item)
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
				.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
            //ViewBag.CategoryList = CategoryList; //asp-items="ViewBag.CategoryList"
            //ViewData["CategoryList"] = CategoryList; //asp-items="@(ViewData["CategoryList"] as IEnumerable<SelectListItem>)"
            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //create
				return View(productVM);
			}else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id); 
                return View(productVM);
            }
        }

        //Adding new row in database
        //POST
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            //server side validation
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
                //ModelState.AddModelError("name","The Display Order cannot exactyl match the Name.");
            //}
            

            if (ModelState.IsValid)
            {
                //get root path
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) 
                    {
                        //delete the old image
                        var oldImagePath = 
                            Path.Combine(wwwRootPath,productVM.Product.ImageUrl.TrimStart('\\'));

                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream); 
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;

                }
                if(productVM.Product.Id == 0)
                {
					_unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
					_unitOfWork.Product.Update(productVM.Product);
				}
                
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
				//projections(converting category to select list item)
				productVM.CategoryList = _unitOfWork.Category
                    .GetAll().Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
				return View(productVM);
			}
        }

        ////implementation of Delete button
        ////GET
        //public IActionResult Delete(int? id) //we need id user wants to edit
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Product? ProductFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    //Product? ProductFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);
        //}

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
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objProductList});
        }

        #endregion

        #region API CALLS

        [HttpDelete]
		public IActionResult Delete(int? id)
		{
            var productToBeDeleted = _unitOfWork.Product.Get(u =>u.Id == id);

            if (productToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

			//delete the old image
			var oldImagePath =
				Path.Combine(_webHostEnvironment.WebRootPath, 
                productToBeDeleted.ImageUrl.TrimStart('\\'));

			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

			return Json(new {success = true, message = "Delete Successful"});
		}

		#endregion
	}
}
