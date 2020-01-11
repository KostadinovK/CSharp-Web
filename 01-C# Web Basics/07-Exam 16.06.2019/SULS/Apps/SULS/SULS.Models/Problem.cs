using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SULS.Models
{
    public class Problem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(50, 300)]
        public int Points { get; set; }

        public ICollection<Submission> Submissions { get; set; } = new HashSet<Submission>();
    }
}
