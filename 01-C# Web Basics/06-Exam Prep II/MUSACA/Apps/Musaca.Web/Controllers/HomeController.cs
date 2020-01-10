using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Musaca.Services;
using Musaca.Web.ViewModels;
using Musaca.Web.ViewModels.Product;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace Musaca.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;

        public HomeController(IOrderService orderService, IProductService productService)
        {
            this.orderService = orderService;
            this.productService = productService;
        }

        [HttpGet(Url = "/")]
        public IActionResult IndexSlash()
        {
            return this.Redirect("/Home/Index");
        }

        public IActionResult Index()
        {
            if (User == null)
            {
                return this.View("GuestHome");
            }

            var orderId = orderService.GetUserActiveOrderId(User.Id);

            var products = productService.GetAllProductsInOrder(orderId)
                .Select(p => new ProductViewModel
                {
                    Name = p.Name,
                    Price = $"{p.Price:f2}"
                })
                .ToList();

            var totalPrice = productService.GetTotalOrderPrice(orderId);

            return this.View(new OrderViewModel{ Products = products, TotalPrice = totalPrice }, "UserHome");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Cashout()
        {
            var orderId = orderService.GetUserActiveOrderId(User.Id);

            orderService.Cashout(orderId);
            orderService.CreateOrder(User.Id);

            return Redirect("/");
        }
    }
}
