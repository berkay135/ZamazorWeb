using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZamazorWebRazor_Temp.Data;
using ZamazorWebRazor_Temp.Models;

namespace ZamazorWebRazor_Temp.Pages.Categories
{
        [BindProperties]
        public class DeleteModel : PageModel
        {
            private readonly ApplicationDbContext _db;
            //property
            //we will be working with one category
            //[BindProperty]
            public Category Category { get; set; }
            //constructor
            public DeleteModel(ApplicationDbContext db)
            {
                _db = db;
            }

            public void OnGet(int? id)
            {
                if (id != null && id != 0)
                {
                    Category = _db.Categories.Find(id);
                }
            }

            public IActionResult OnPost()
            {
                Category? obj = _db.Categories.Find(Category.Id); 
                if (obj == null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToPage("Index");
            }   
        }
}
