using System;
using System.Collections.Generic;
using System.Text;
using Musaca.Services;
using SIS.MvcFramework;
using SIS.MvcFramework.Routing;
using IServiceProvider = SIS.MvcFramework.DependencyContainer.IServiceProvider;

namespace Musaca.Web
{
    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUserService, UserService>();
            serviceProvider.Add<IProductService, ProductService>();
            serviceProvider.Add<IOrderService, OrderService>();
        }
    }
}
