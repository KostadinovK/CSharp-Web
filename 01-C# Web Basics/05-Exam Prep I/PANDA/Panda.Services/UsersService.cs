using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Panda.Data;
using Panda.Data.Models;

namespace Panda.Services
{
    public class UsersService : IUsersService
    {
        private PandaDbContext context;

        public UsersService(PandaDbContext context)
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
                Username = username,
                Email = email,
                Password = HashPassword(password)
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        public User GetUserOrNull(string username, string password)
        {
            var hashPass = HashPassword(password);

            return context.Users.SingleOrDefault(u => u.Username == username && u.Password == hashPass);
        }
    }
}
