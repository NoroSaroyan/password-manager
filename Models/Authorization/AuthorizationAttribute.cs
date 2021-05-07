using Microsoft.AspNetCore.Mvc;
using PasswordManager.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordManager.Models.Authorization
{
    public class AuthorizationAttribute : Attribute 
    {
        private readonly LoginController controller = new LoginController();
        public bool Authorize(Guid id)
        {
            if (id.Equals(null))
            {
                controller.SignIn();
            }
                return true;
        }
    }
}
