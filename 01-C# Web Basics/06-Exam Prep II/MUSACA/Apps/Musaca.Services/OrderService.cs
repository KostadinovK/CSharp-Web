using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Musaca.Data;
using Musaca.Models;
using Musaca.Models.Enums;

namespace Musaca.Services
{
    public class OrderService : IOrderService
    {
        private readonly MusacaDbContext context;

        public OrderService(MusacaDbContext context)
        {
            this.context = context;
        }

        public string CreateOrder(string userId)
        {
            var order = new Order
            {
                CashierId = userId,
                Status = OrderStatus.Active
            };

            context.Orders.Add(order);
            context.SaveChanges();

            return order.Id;
        }

        public bool AddProductToActiveOrder(string productId, string userId)
        {
            var product = context.Products.Find(productId);

            var order = context.Orders.SingleOrDefault(o => o.CashierId == userId && o.Status == OrderStatus.Active);

            if (product == null || order == null)
            {
                return false;
            }

            var orderProduct = new OrderProduct()
            {
                OrderId = order.Id,
                ProductId = product.Id
            };

            order.Products.Add(orderProduct);
            context.OrdersProducts.Add(orderProduct);
            context.Update(order);
            context.SaveChanges();

            return true;
        }

        public string GetUserActiveOrderId(string userId)
        {
            var order = context.Orders.FirstOrDefault(o => o.CashierId == userId && o.Status == OrderStatus.Active);

            return order.Id;
        }

        public bool Cashout(string orderId)
        {
            var order = context.Orders.Find(orderId);

            if (order == null)
            {
                return false;
            }

            order.Status = OrderStatus.Completed;
            order.IssuedOn = DateTime.UtcNow;

            context.Update(order);
            context.SaveChanges();

            return true;
        }

        public IQueryable<Order> GetAllCompletedOrdersForUser(string userId)
        {
            return context.Orders.Where(o => o.CashierId == userId && o.Status == OrderStatus.Completed).AsQueryable();
        }
    }
}
