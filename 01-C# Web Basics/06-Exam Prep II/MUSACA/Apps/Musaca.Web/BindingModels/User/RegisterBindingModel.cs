using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace Musaca.Web.BindingModels.User
{
    public class RegisterBindingModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username must be between 5 and 20 characters")]
        public string Username { get; set; }

        [RequiredSis]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }

        [RequiredSis]
        [StringLengthSis(5, 20, "Email must be between 5 and 20 characters")]
        public string Email { get; set; }
    }
}
