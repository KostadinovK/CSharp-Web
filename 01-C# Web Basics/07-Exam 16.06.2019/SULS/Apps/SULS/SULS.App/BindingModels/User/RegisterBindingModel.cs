using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.BindingModels.User
{
    public class RegisterBindingModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Username must be between 5 and 20 characters long")]
        public string Username { get; set; }

        [RequiredSis]
        [EmailSis]
        public string Email { get; set; }

        [RequiredSis]
        [StringLengthSis(6, 20, "Password must be between 6 and 20 characters long")]
        public string Password { get; set; }

        [RequiredSis]
        public string ConfirmPassword { get; set; }
    }
}
