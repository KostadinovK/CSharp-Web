using System;
using System.Collections.Generic;
using System.Text;
using Musaca.Web.ViewModels.Product;

namespace Musaca.Web.ViewModels
{
    public class OrderViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();

        public decimal TotalPrice { get; set; }
    }
}
