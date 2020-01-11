using System;
using System.Collections.Generic;
using System.Text;
using SULS.App.ViewModels.Submissions;

namespace SULS.App.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public List<SubmissionViewModel> Submissions { get; set; }

        public string Name { get; set; }

        public int MaxPoints { get; set; }
    }
}
