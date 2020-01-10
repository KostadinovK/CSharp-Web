using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace Musaca.Web.BindingModels.Product
{
    public class CreateBindingModel
    {
        [RequiredSis]
        [StringLengthSis(3, 10, "Name must be between 5 and 10 characters long")]
        public string Name { get; set; }
        
        [RangeSis(typeof(decimal), "0.01", "10000000", "Price must be minimum 0.01")]
        public decimal Price { get; set; }
    }
}
