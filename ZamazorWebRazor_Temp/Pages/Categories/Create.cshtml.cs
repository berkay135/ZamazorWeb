using Microsoft.AspNetCore.Mvc.RazorPages;
using ZamazorWebRazor_Temp.Data;
using ZamazorWebRazor_Temp.Models;

namespace ZamazorWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //property
        //we will be working with one category
        public Category CategoryList { get; set; }
        //constructor
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
    }
}
