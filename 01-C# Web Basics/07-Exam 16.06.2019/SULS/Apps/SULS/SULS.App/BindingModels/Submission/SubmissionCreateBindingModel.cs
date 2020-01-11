using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.BindingModels.Submission
{
    public class SubmissionCreateBindingModel
    {
        [RequiredSis]
        [StringLengthSis(30, 800, "Code must be between 30 and 800 characters")]
        public string Code { get; set; }

        [RequiredSis]
        public string ProblemId { get; set; }
    }
}
