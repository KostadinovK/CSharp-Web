using System;
using SIS.MvcFramework;

namespace Panda.App
{
    public class Program
    {
        public static void Main()
        {
            WebHost.Start(new StartUp());
        }
    }
}
