using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;
using Panda.Services;
using Panda.Web.ViewModels.Users;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Result;

namespace Panda.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/Login");
            }

            var user = usersService.GetUserOrNull(input.Username, input.Password);
            
            if (user == null)
            {
                return Redirect("/Users/Login");
            }

            SignIn(user.Id, user.Username, user.Email);

            return Redirect("/");
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Users/Register");
            }

            if (input.ConfirmPassword != input.Password)
            {
                return Redirect("/Users/Register");
            }

            var userId = usersService.CreateUser(input.Username, input.Email, input.Password);

            return Redirect("/Users/Login");
        }

        public IActionResult Logout()
        {
            SignOut();
            return Redirect("/");
        }
    }
}
