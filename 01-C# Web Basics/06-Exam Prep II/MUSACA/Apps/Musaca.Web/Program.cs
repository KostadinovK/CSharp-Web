using System;
using SIS.MvcFramework;

namespace Musaca.Web
{
    public class Program
    {
        public static void Main()
        {
            WebHost.Start(new Startup());
        }
    }
}
