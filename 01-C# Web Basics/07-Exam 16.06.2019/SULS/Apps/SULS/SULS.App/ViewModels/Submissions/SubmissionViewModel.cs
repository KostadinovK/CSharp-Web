using System;
using System.Collections.Generic;
using System.Text;

namespace SULS.App.ViewModels.Submissions
{
    public class SubmissionViewModel
    {
        public string Id { get; set; }
        
        public string User { get; set; }

        public int AchievedResult { get; set; }

        public string CreatedOn { get; set; }
    }
}
