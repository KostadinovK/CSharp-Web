using System;
using System.Collections.Generic;
using System.Text;
using Panda.Data.Models;

namespace Panda.Services
{
    public interface IUsersService
    {
        string CreateUser(string username, string email, string password);

        User GetUserOrNull(string username, string password);

        string GetUserId(string username);

        IEnumerable<string> GetUsernames();
    }
}
