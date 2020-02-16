using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SIS.MvcFramework;

namespace SharedTrip.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(20)]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IdentityRole Role { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();
    }
}
