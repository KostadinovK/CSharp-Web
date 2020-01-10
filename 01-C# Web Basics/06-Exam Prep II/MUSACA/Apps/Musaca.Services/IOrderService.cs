using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Musaca.Models;

namespace Musaca.Services
{
    public interface IOrderService
    {
        string CreateOrder(string userId);

        bool AddProductToActiveOrder(string productId, string userId);

        string GetUserActiveOrderId(string userId);

        bool Cashout(string orderId);

        IQueryable<Order> GetAllCompletedOrdersForUser(string userId);
    }
}
