using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZamazorWebRazor_Temp.Data;
using ZamazorWebRazor_Temp.Models;

namespace ZamazorWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //property
        public List<Category> CategoryList { get; set; }
        //constructor
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
