using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.Models.Models
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
