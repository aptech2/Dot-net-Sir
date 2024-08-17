using System.Data;

using Danish_Project_Dot_net.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Danish_Project_Dot_net.Controllers
{
    public class AdminController : Controller
    {
        
        DanishContext db = new DanishContext();
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
