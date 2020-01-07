using System;
using System.ComponentModel.DataAnnotations;
using Panda.Data.Enums;

namespace Panda.Data.Models
{
    public class Package
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }

        public decimal? Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime? EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; } = Guid.NewGuid().ToString();

        public User Recipient { get; set; }
    }
}
