﻿using System;
using System.Collections.Generic;
using System.Text;
using IRunes.App.Controllers;
using IRunes.Data;
using SIS.HTTP.Enums;
using SIS.MvcFramework;
using SIS.WebServer;
using SIS.WebServer.Routing;

namespace IRunes.App
{
    public class Startup : IMvcApplication
    {
        public void Configure(IServerRoutingTable serverRoutingTable)
        {
            using (var context = new RunesDbContext())
            {
                context.Database.EnsureCreated();
            }

        }

        public void ConfigureServices()
        {
        }
    }
}
