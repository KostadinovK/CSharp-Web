using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Models.Models;

namespace IRunes.Services
{
    public interface IUserService
    {
        User CreateUser(User user);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}
