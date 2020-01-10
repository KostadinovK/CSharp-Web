using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Musaca.Models;
using Musaca.Services;
using Musaca.Web.BindingModels.Product;
using Musaca.Web.ViewModels.Product;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;

namespace Musaca.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;

        public ProductsController(IProductService productService, IOrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateBindingModel input)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/Products/Create");
            }

            var productId = productService.CreateProduct(input.Name, input.Price);

            if (productId == null)
            {
                return this.Redirect("/Products/Create");
            }

            return this.Redirect("/Products/All");
        }

        [Authorize]
        public IActionResult All()
        {
            var products = productService.GetAll()
                .Select(p => new ProductViewModel
                {
                    Name = p.Name,
                    Price = $"{p.Price:f2}"
                })
                .ToList();

            return this.View(products);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Order(ProductOrderBindingModel input)
        {
            string productId = productService.GetId(input.Name);

            orderService.AddProductToActiveOrder(productId, User.Id);

            return this.Redirect("/");
        }
    }
}
