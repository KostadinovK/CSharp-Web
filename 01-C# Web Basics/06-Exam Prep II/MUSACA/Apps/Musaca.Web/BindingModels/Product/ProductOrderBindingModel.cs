using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework.Attributes.Validation;

namespace Musaca.Web.BindingModels.Product
{
    public class ProductOrderBindingModel
    {
        [RequiredSis]
        public string Name { get; set; }
    }
}
