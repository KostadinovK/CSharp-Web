using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Http;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private string ComputeSha256Hash(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();
            
            return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public IHttpResponse Register(IHttpRequest httpRequest)
        {
            return this.View();
        }

        [HttpPost(ActionName = "Register")]
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

                context.Users.Add(user);
                context.SaveChanges();
            }

            return Redirect("/Users/Login");
        }

        public IHttpResponse Login(IHttpRequest httpRequest)
        {
            return this.View();
        }

        [HttpPost(ActionName = "Login")]
        public IHttpResponse LoginConfirm(IHttpRequest httpRequest)
        {
            using (var context = new RunesDbContext())
            {
                var username = ((ISet<string>)httpRequest.FormData["username"]).FirstOrDefault();
                var password = ((ISet<string>)httpRequest.FormData["password"]).FirstOrDefault();
                
                var user = context.Users.FirstOrDefault(u => (u.Username == username && u.Password == ComputeSha256Hash(password)) || (u.Email == username && u.Password == ComputeSha256Hash(password)));

                if (user == null)
                {
                    return Redirect("/Users/Register");
                }

                SignIn(httpRequest, user.Id, user.Username, user.Email);
            }

            return Redirect("/Home/Index");
        }

        public IHttpResponse Logout(IHttpRequest httpRequest)
        {
            SignOut(httpRequest);

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
