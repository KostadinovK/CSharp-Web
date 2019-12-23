using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IRunes.Data;
using IRunes.Models.Models;

namespace IRunes.Services
{
    public class UserService : IUserService
    {
        private readonly RunesDbContext context;

        public UserService()
        {
            context = new RunesDbContext();
        }

        public User CreateUser(User user)
        {
            user = context.Users.Add(user).Entity;
            context.SaveChanges();

            return user;
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return context.Users.SingleOrDefault(u =>
                (u.Username == username || u.Email == username) && u.Password == password);
        }
    }
}
