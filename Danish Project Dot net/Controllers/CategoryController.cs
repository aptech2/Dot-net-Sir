using Danish_Project_Dot_net.Models;

using Microsoft.AspNetCore.Mvc;

namespace Danish_Project_Dot_net.Controllers
{
    public class CategoryController : Controller
    {
        DanishContext db = new DanishContext();
        public IActionResult Index()
        {
            var category = db.Categories.ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                ViewBag.msg = "Category Add Sucessfully";
                return RedirectToAction("Index");
            }
            else
            {
               return View();
            }
        }

        public IActionResult Edit(int ID)
        {
            var cat = db.Categories.Find(ID);
            return View(cat);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Category cat)
        {
              db.Categories.Update(cat);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int ID)
        {
            var cat = db.Categories.Find(ID);
            return View(cat);
        }

        [HttpPost]
        public IActionResult Delete(Category cat)
        {
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int ID)
        {
            var cat = db.Categories.Find(ID);
            return View(cat);
        }
    }
}
