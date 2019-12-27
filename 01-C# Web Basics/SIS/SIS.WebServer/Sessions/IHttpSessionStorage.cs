using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Sessions;

namespace SIS.MvcFramework.Sessions
{
    public interface IHttpSessionStorage
    {
        IHttpSession GetSession(string sessionId);

        bool ContainsSession(string sessionId);
    }
}
