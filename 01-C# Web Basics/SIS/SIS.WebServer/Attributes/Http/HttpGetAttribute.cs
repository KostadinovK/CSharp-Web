using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Enums;
using SIS.MvcFramework.Attributes.Http;

namespace SIS.MvcFramework.Attributes.Http
{
    public class HttpGetAttribute : BaseHttpAttribute
    {
        public override HttpRequestMethod Method => HttpRequestMethod.Get;
    }
}
