using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Zamazor.DataAccess.Repos.IRepository;
using Zamazor.Models;
using Zamazor.Models.ViewModels;
using Zamazor.Utility;
using ZamazorWeb.DataAccess.Data;

namespace ZamazorWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        //Implementation of ApplicatonDbContext
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //Retrieve companys from sql server and pass object to view
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
			return View(objCompanyList);
        }
        

        //Implementation of Create button
        //GET
        public IActionResult Upsert(int? id) //create and edit combined
        {
            if(id == null || id == 0) 
            {
                //create
				return View(new Company());
			}else
            {
                //Update
                Company companyObj = _unitOfWork.Company.Get(u=>u.id==id); 
                return View(companyObj);
            }
        }

        //Adding new row in database
        //POST
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            //server side validation
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
                //ModelState.AddModelError("name","The Display Order cannot exactyl match the Name.");
            //}
            if (ModelState.IsValid)
            {
                if (CompanyObj.id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";
                return RedirectToAction("Index");
            }   
            else
            {
				return View(CompanyObj);
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
        //    Company? CompanyFromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? CompanyFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    //Company? CompanyFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (CompanyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(CompanyFromDb);
        //}

        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Company? obj = _unitOfWork.Company.Get(u => u.id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successfully";
            return RedirectToAction("Index");
        }
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data = objCompanyList});
        }

        #endregion

        #region API CALLS

        [HttpDelete]
		public IActionResult Delete(int? id)
		{
            var companyToBeDeleted = _unitOfWork.Company.Get(u =>u.id == id);

            if (companyToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting"});
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

			return Json(new {success = true, message = "Delete Successful"});
		}

		#endregion
	}
}
