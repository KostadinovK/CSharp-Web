using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SULS.Models
{
    public class Submission
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(800)]
        public string Code { get; set; }

        [Required]
        [Range(0, 300)]
        public int AchievedResult { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
