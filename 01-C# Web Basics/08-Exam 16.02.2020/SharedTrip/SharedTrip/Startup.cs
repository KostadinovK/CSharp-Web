using SharedTrip.Services;
using SharedTrip.Services.Implementations;
using SharedTrip.Services.Interfaces;

namespace SharedTrip
{
    using System.Collections.Generic;

    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
           var context = new ApplicationDbContext();
           context.Database.EnsureCreated();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
            serviceCollection.Add<ITripService, TripService>();
        }
    }
}
