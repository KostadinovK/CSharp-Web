using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Data;
using Demo.Data.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace SIS.Demo.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IHttpRequest request) : base(request) { }

        public IHttpResponse Login()
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm()
        {
            using (var context = new DemoDbContext())
            {
                string username = HttpRequest.FormData["username"].ToString();
                string password = HttpRequest.FormData["password"].ToString();

                string encryptedPassword = PasswordManager.Base64Encode(password);

                var user = context.Users.SingleOrDefault(user =>
                    user.Username == username && encryptedPassword == user.Password);

                if (user == null)
                {
                    this.Redirect("/login");
                }

                HttpRequest.Session.AddParameter("username", user.Username);
                ViewData.Add("HelloMessage", $"Hello, {HttpRequest.Session.GetParameter("username")}");
            }

            return Redirect("/");
        }

        public IHttpResponse Register()
        { 
            return this.View();
        }

        public IHttpResponse RegisterConfirm()
        {
            using (var context = new DemoDbContext())
            {
                string username = HttpRequest.FormData["username"].ToString();
                string password = HttpRequest.FormData["password"].ToString();
                string confirmPassword = HttpRequest.FormData["confirmPassword"].ToString();

                if (password != confirmPassword)
                {
                    return Redirect("/register");
                }

                string encryptedPassword = PasswordManager.Base64Encode(password);

                var user = new User
                {
                    Username = username,
                    Password = encryptedPassword
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            return Redirect("/login");
        }

        public IHttpResponse Logout()
        {
            if (IsLoggedIn())
            {
                HttpRequest.Session.ClearParameters();
            }

            return Redirect("/");
        }
    }
}
