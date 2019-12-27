using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SIS.HTTP.Enums;
using SIS.HTTP.Responses;
using SIS.MvcFramework.Attributes.Action;
using SIS.MvcFramework.Attributes.Http;
using SIS.MvcFramework.Attributes.Security;
using SIS.MvcFramework.Result;
using SIS.WebServer;
using SIS.WebServer.Routing;
using SIS.WebServer.Sessions;

namespace SIS.MvcFramework
{
    public static class WebHost
    {
        public static void Start(IMvcApplication application)
        {
            var serverRoutingTable = new ServerRoutingTable();
            var httpSessionStorage = new HttpSessionStorage();

            AutoRegisterRoutes(application, serverRoutingTable);
            application.ConfigureServices();
            application.Configure(serverRoutingTable);

            var server = new Server(8000, serverRoutingTable, httpSessionStorage);
            server.Run();
        }

        private static void AutoRegisterRoutes(IMvcApplication application, IServerRoutingTable serverRoutingTable)
        {
            var assembly = application.GetType().Assembly;
            var controllers = assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(Controller).IsAssignableFrom(t))
                .ToList();

            foreach (var controller in controllers)
            {
                var controllerName = controller.Name.Replace("Controller", "");

                var actions = controller
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(m => m.ReturnType == typeof(ActionResult) && !m.IsVirtual && m.GetCustomAttributes().All(attribute => attribute.GetType() != typeof(NonActionAttribute)))
                    .ToList();

                foreach (var action in actions)
                {
                    var actionName = action.Name;
                    var path = $"/{controllerName}/{actionName}";

                    var attribute =
                        action.GetCustomAttributes().LastOrDefault(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute))) as BaseHttpAttribute;

                    var httpMethod = HttpRequestMethod.Get;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (attribute?.Url != null)
                    {
                        path = attribute.Url;
                    }

                    if (attribute?.ActionName != null)
                    {
                        path = $"/{controllerName}/{attribute.ActionName}";
                    }

                    Console.WriteLine($"{httpMethod} - {path}");
                    serverRoutingTable.Add(httpMethod, path, request => 
                    {
                        var controllerInstance = Activator.CreateInstance(controller);
                        ((Controller) controllerInstance).Request = request;
                        var response = action.Invoke(controllerInstance, new object[0]) as ActionResult;

                        var controllerPrincipal = ((Controller) controllerInstance).User;
                        var authorizeAttribute =
                            action.GetCustomAttributes().LastOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                                as AuthorizeAttribute;

                        if (authorizeAttribute != null && !authorizeAttribute.IsInAuthority(controllerPrincipal))
                        {
                            return new HttpResponse(HttpResponseStatusCode.Forbidden);
                        }

                        return response;
                    });
                }
            }
        }
    }
}
