using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.Models.Models
{
    public class Track
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }

        public string AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
