using System;
using System.Collections.Generic;
using System.Text;
using SULS.Models;

namespace SULS.Services
{
    public interface IUserService
    {
        string CreateUser(string username, string email, string password);

        User GetByUsernameAndPassword(string username, string password);
    }
}
