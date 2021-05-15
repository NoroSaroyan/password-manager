using Microsoft.AspNetCore.Mvc;
using PasswordManager.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PasswordManager.Controllers
{
    public class LoginController : Controller
    {
        public static User currentUser;
        public static string j = "a";
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(User user)
        {

            if (user.Login())
            {
                currentUser = user;
                this.HttpContext.Request.Headers.Add("user-id", user.UserName);
                var home = View("../Home/Index", user.GetAccounts());
                return home;
            }
            return View("../Login/Index");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            return View("");
        }
    }
}
