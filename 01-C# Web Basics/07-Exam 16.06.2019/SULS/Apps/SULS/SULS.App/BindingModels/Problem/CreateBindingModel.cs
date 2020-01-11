using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace SULS.App.BindingModels.Problem
{
    public class CreateBindingModel
    {
        [RequiredSis]
        [StringLengthSis(5, 20, "Name must be between 5 and 20 characters long")]
        public string Name { get; set; }

        [RequiredSis]
        [RangeSis(50, 300, "Points must be a number between 50 and 300")]
        public int Points { get; set; }
    }
}
