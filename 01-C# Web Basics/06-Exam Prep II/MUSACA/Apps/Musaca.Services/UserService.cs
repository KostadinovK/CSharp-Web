using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class UserService : IUserService
    {
        private readonly MusacaDbContext context;

        public UserService(MusacaDbContext context)
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

        public string CreateUser(string username, string password, string email)
        {
            if (context.Users.Any(u => u.Username == username))
            {
                return null;
            }

            var hashPass = HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashPass,
                Email = email
            };

            context.Users.Add(user);
            context.SaveChanges();

            return user.Id;
        }

        public User GetUserOrNull(string username, string password)
        {
            var user = context.Users.SingleOrDefault(
                u => u.Username == username && u.Password == HashPassword(password));

            return user;
        }
    }
}
