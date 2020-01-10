using System;
using System.Collections.Generic;
using System.Text;

namespace Musaca.Web.ViewModels.Order
{
    public class OrderProfileViewModel
    {
        public string Id { get; set; }

        public string Total { get; set; }

        public string IssuedOn { get; set; }

        public string Cashier { get; set; }
    }
}
