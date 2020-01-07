﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Data.Models
{
    public class Receipt
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        [Required]
        public string RecipientId { get; set; } = Guid.NewGuid().ToString();

        public User Recipient { get; set; }

        [Required]
        public string PackageId { get; set; } = Guid.NewGuid().ToString();

        public Package Package { get; set; }
    }
}
