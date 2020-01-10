using System;
using System.Collections.Generic;
using System.Text;
using Musaca.Models;

namespace Musaca.Services
{
    public interface IUserService
    {
        string CreateUser(string username, string password, string email);

        User GetUserOrNull(string username, string password);
    }
}
