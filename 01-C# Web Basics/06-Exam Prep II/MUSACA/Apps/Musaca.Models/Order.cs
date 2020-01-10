using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Text;
using Musaca.Models.Enums;

namespace Musaca.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public OrderStatus Status { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<OrderProduct> Products { get; set; } = new HashSet<OrderProduct>();

        [Required]
        public string CashierId { get; set; }

        public User Cashier { get; set; }
    }
}
