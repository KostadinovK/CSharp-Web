using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Musaca.Models;

namespace Musaca.Services
{
    public interface IProductService
    {
        string CreateProduct(string name, decimal price);

        IQueryable<Product> GetAll();

        string GetId(string name);

        IQueryable<Product> GetAllProductsInOrder(string orderId);

        decimal GetTotalOrderPrice(string orderId);
    }
}
