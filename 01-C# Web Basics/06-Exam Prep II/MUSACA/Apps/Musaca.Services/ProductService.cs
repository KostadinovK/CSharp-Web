using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Musaca.Data;
using Musaca.Models;

namespace Musaca.Services
{
    public class ProductService : IProductService
    {
        private readonly MusacaDbContext context;

        public ProductService(MusacaDbContext context)
        {
            this.context = context;
        }

        public string CreateProduct(string name, decimal price)
        {
            if (context.Products.Any(p => p.Name == name && p.Price == price))
            {
                return null;
            }

            var product = new Product
            {
                Name = name,
                Price = price
            };

            context.Products.Add(product);
            context.SaveChanges();

            return product.Id;
        }

        public IQueryable<Product> GetAll()
        {
            return context.Products.AsQueryable();
        }

        public string GetId(string name)
        {
            var product = context.Products.Where(p => p.Name == name).SingleOrDefault();

            return product?.Id;
        }

        public IQueryable<Product> GetAllProductsInOrder(string orderId)
        {
            return context.OrdersProducts.Where(op => op.OrderId == orderId).Select(op => op.Product).AsQueryable();
        }

        public decimal GetTotalOrderPrice(string orderId)
        {
            return context.OrdersProducts.Where(op => op.OrderId == orderId).Sum(p => p.Product.Price);
        }
    }
}
