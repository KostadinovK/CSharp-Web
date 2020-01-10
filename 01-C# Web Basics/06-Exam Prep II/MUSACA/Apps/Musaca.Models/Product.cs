using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Musaca.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Range(typeof(decimal), "0.01", "1000000000")]
        public decimal Price { get; set; }
    }
}
