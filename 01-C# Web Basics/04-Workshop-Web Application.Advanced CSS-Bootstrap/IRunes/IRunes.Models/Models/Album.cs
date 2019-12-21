using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IRunes.Models.Models
{
    public class Album
    {
        public string Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Cover { get; set; }

        public decimal Price { get; set; }

        public ICollection<Track> Tracks { get; set; } = new HashSet<Track>();
    }
}
