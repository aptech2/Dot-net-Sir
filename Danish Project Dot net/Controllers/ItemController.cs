using Microsoft.AspNetCore.Mvc;

namespace Danish_Project_Dot_net.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
