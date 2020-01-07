using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace Panda.Web.ViewModels.Users
{
    public class RegisterInputModel
    {
        [RequiredSis("Username is required")]
        [StringLengthSis(5, 20, "Username must be between 5 and 20 characters long")]
        public string Username { get; set; }

        [RequiredSis("Email is required")]
        [StringLengthSis(5, 20, "Email must be between 5 and 20 characters long")]
        public string Email { get; set; }

        [RequiredSis]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
