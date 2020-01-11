using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SULS.Data;
using SULS.Models;

namespace SULS.Services
{
    public class UserService : IUserService
    {
        private readonly SULSContext context;

        public UserService(SULSContext context)
        {
            this.context = context;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return Encoding.UTF8.GetString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public string CreateUser(string username, string email, string password)
        {
            if (context.Users.Any(u => u.Username == username))
            {
                return null;
            }

            var user = new User
            {
                Email = email,
                Username = username,
                Password = HashPassword(password)
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return context.Users.SingleOrDefault(u => u.Username == username && u.Password == HashPassword(password));
        }
    }
}
