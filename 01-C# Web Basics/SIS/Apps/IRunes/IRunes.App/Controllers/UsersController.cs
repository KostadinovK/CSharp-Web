using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using IRunes.Services;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Result;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService userService;

        public UsersController()
        {
            userService = new UserService();
        }

        private string ComputeSha256Hash(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            
            return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost(ActionName = "Register")]
        public ActionResult RegisterConfirm()
        {
            var username = ((ISet<string>)Request.FormData["username"]).FirstOrDefault();
            var password = ((ISet<string>)Request.FormData["password"]).FirstOrDefault();
            var confirmPassword = ((ISet<string>)Request.FormData["confirmPassword"]).FirstOrDefault();
            var email = ((ISet<string>)Request.FormData["email"]).FirstOrDefault();

            if (password != confirmPassword)
            {
                return Redirect("/Users/Register");
            }

            var passwordHashed = ComputeSha256Hash(password);

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = username,
                Password = passwordHashed,
                Email = email
            };

            if (!IsValid(user))
            {
                return Redirect("/Users/Register");
            }

            userService.CreateUser(user);

            return Redirect("/Users/Login");
        }

        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost(ActionName = "Login")]
        public ActionResult LoginConfirm()
        {
            var username = ((ISet<string>)Request.FormData["username"]).FirstOrDefault();
            var password = ((ISet<string>)Request.FormData["password"]).FirstOrDefault();

            var user = userService.GetUserByUsernameAndPassword(username, ComputeSha256Hash(password));

            if (user == null)
            {
                return Redirect("/Users/Register");
            }

            SignIn(user.Id, user.Username, user.Email);


            return Redirect("/Home/Index");
        }

        public ActionResult Logout()
        {
            SignOut();

            return Redirect("/Home/Index");
        }

        protected bool IsValid(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);


            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
