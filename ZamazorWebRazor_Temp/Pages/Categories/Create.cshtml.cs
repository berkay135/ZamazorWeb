using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZamazorWebRazor_Temp.Data;
using ZamazorWebRazor_Temp.Models;

namespace ZamazorWebRazor_Temp.Pages.Categories
{
    //[BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //property
        //we will be working with one category
        [BindProperty]
        public Category Category { get; set; }
        //constructor
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost() 
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
