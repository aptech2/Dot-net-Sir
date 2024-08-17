using System.Security.Claims;

using Danish_Project_Dot_net.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Danish_Project_Dot_net.Controllers
{
   
    public class AuthController : Controller
    {

        DanishContext db =  new DanishContext();

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User use )
        {
            var check = db.Users.FirstOrDefault(a=>a.Email == use.Email);
            if (check == null)
            {
                var hasher = new PasswordHasher<string>();
                string hashpassword = hasher.HashPassword(use.Email,use.Password);
                use.Password = hashpassword;
               
                db.Users.Add(use);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.msg = "User already exist. Please Sign up";
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(User user)
        {
            var controller = "";
            bool isAuthenticated = false;
            ClaimsIdentity identity = null;

            var checkuser = db.Users.FirstOrDefault(a => a.Email == user.Email);

            if (checkuser != null)
            {
                var hasher = new PasswordHasher<string>();
                var verifypassword = hasher.VerifyHashedPassword(user.Email, checkuser.Password, user.Password);

                if (verifypassword == PasswordVerificationResult.Success &&
                   checkuser.RoleId == 2)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name ,checkuser.Name),
                    new Claim(ClaimTypes.Role ,"user"),
                    new Claim(ClaimTypes.Sid,checkuser.Id.ToString())
                },
                CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticated = true;
                    controller = "User";
                    HttpContext.Session.SetInt32("UserId",checkuser.Id);
                    HttpContext.Session.SetString("UserEmail", checkuser.Email);
                }
                else if (verifypassword == PasswordVerificationResult.Success &&
                   checkuser.RoleId == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name ,checkuser.Name),
                    new Claim(ClaimTypes.Role ,"Admin"),
                    new Claim(ClaimTypes.Sid,checkuser.Id.ToString())

                }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticated = true;
                    controller = "Admin";
                    HttpContext.Session.SetInt32("UserId", checkuser.Id);
                    HttpContext.Session.SetString("UserEmail", checkuser.Email);
                }
                else
                {

                    ViewBag.msg = "Invalid Credentials";
                    return View();
                }
                if (isAuthenticated)
                {
                    var principle = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);
                    return RedirectToAction("Index", controller);
                }
            }
                return View();

        }

        public IActionResult Logout()
        {
            //HttpContext.Session.Remove(UserID);
            //HttpContext.Session.Remove(UserEmail);
            HttpContext.Session.Clear();

            var login = HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
