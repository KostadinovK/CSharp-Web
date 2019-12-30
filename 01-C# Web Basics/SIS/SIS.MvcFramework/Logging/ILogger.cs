using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework.Logging
{
    public interface ILogger
    {
        void Log(string message);
    }
}
