using System;
using System.Collections.Generic;
using System.Text;
using Panda.Services;
using SIS.MvcFramework;
using SIS.MvcFramework.Routing;
using IServiceProvider = SIS.MvcFramework.DependencyContainer.IServiceProvider;

namespace Panda.App
{
    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUsersService, UsersService>();
            serviceProvider.Add<IReceiptsService, ReceiptsService>();
            serviceProvider.Add<IPackageService, PackageService>();
        }
    }
}
