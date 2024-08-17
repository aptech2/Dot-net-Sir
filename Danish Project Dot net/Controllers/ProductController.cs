using Danish_Project_Dot_net.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Danish_Project_Dot_net.Controllers
{
    public class ProductController : Controller
    {
        DanishContext Db = new DanishContext();
        public IActionResult Index()
        {
            var data = Db.Products.Include(pro => pro.Cat);
            return View(Db.Products.ToList());
        }
        public IActionResult Create()
        {
            ViewBag.CatId = new SelectList(Db.Categories, "cat_id", "name");
            return View();
        }
    }
}
