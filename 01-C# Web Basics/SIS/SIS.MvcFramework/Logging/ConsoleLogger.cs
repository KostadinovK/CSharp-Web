using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SIS.MvcFramework.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:yy-MM-dd}] [{Thread.CurrentThread.ManagedThreadId}] {message}");
        }
    }
}
