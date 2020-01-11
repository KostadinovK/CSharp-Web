using SULS.Services;

namespace SULS.App
{
    using Data;
    using SIS.MvcFramework;
    using SIS.MvcFramework.DependencyContainer;
    using SIS.MvcFramework.Routing;

    public class StartUp : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
        }

        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            serviceProvider.Add<IUserService, UserService>();
            serviceProvider.Add<IProblemService, ProblemService>();
            serviceProvider.Add<ISubmissionService, SubmissionService>();
        }
    }
}