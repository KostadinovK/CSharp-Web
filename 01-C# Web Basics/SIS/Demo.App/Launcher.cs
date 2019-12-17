using SIS.Demo.Controllers;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Routing.Contracts;

namespace SIS.Demo
{
    public class Launcher
    {
        static void Main()
        {
            IServerRoutingTable serverRoutingTable = new ServerRoutingTable();

            // GET Mappings
            serverRoutingTable.Add(HttpRequestMethod.Get, "/", request => new HomeController(request).Home());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/register", request => new UsersController(request).Register());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/login", request => new UsersController(request).Login());
            serverRoutingTable.Add(HttpRequestMethod.Get, "/logout", request => new UsersController(request).Logout());

            //POST Mappings
            serverRoutingTable.Add(HttpRequestMethod.Post, "/register", request => new UsersController(request).RegisterConfirm());
            serverRoutingTable.Add(HttpRequestMethod.Post, "/login", request => new UsersController(request).LoginConfirm());

            Server server = new Server(8000, serverRoutingTable);
            server.Run();
        }
    }
}
