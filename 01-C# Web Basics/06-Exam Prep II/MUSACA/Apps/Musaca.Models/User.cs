using System;
using System.ComponentModel.DataAnnotations;

namespace Musaca.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
