using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;

namespace IRunes.App
{
    public static class Program
    {
        public static void Main()
        {
            WebHost.Start(new Startup());
        }
    }
}
