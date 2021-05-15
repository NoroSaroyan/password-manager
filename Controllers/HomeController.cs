using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using PasswordManager.Models;
using PasswordManager.Models.Authorization;
using PasswordManager.Models.User;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Controllers
{
    [Authorization]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Account acc)
        {
            string currentUserName = LoginController.currentUser.UserName;
            if (Account.CreateAccount(acc, currentUserName))
            {
                Console.WriteLine("Success");
            }
            else
            {
                throw new Exception("Failure");
            }

            return View("../Home/Index" , LoginController.currentUser.GetAccounts());
        }

        public IActionResult Delete(string Id)
        {
            if (Account.Delete(Id))
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failure";
            }
            return View("../Home/Index",LoginController.currentUser.GetAccounts());
        }
        public IActionResult Edit(string id)
        {
           Account acc = LoginController.currentUser.GetAccounts().FirstOrDefault(x => x.Id == id);
            return View("../Home/Edit", acc );
        }
        [HttpPost]
        public IActionResult Edit(Account account)
        {
            User user = LoginController.currentUser;
            if (Account.Edit(account))
            {
                ViewBag.Message = "Success";
            }
            else
            {
                ViewBag.Message = "Failure";
            }
            return View("../Home/Index", LoginController.currentUser.GetAccounts());
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (LoginController.currentUser == null)
            {
                return View("../Login/Index");
            }
            return View("../Home/Index", LoginController.currentUser.GetAccounts());

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
