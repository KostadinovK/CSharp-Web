using System;
using System.Collections.Generic;
using System.Linq;
using IRunes.Data;
using IRunes.Models.Models;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.App.Controllers
{
    public class UsersController : BaseController
    {
        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse RegisterConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                var username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                var password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                var confirmPassword = ((ISet<string>)httpRequest.FormData["confirmPassword"]).FirstOrDefault();
                var email = ((ISet<string>)httpRequest.FormData["email"]).FirstOrDefault();

                if (password != confirmPassword)
                {
                    return Redirect("/Users/Register");
                }

                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = username,
                    Password = password,
                    Email = email
                };

                if (!IsValid(user))
                {
                    return Redirect("/Users/Register");
                }

                context.Users.Add(user);
                context.SaveChanges();
            }

            return Redirect("/Users/Login");
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                var username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                var password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                
                var user = context.Users.FirstOrDefault(u => (u.Username == username && u.Password == password) || (u.Email == username && u.Password == password));

                if (user == null)
                {
                    return Redirect("/Users/Register");
                }

                httpRequest.Session.AddParameter("username", user.Username);
                httpRequest.Session.AddParameter("email", user.Email);
                httpRequest.Session.AddParameter("id", user.Id);
            }

            return Redirect("/Home/Index");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            httpRequest.Session.ClearParameters();

            return Redirect("/Home/Index");
        }
    }
}
