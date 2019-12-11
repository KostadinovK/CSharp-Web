using System;
using System.Globalization;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;

namespace Demo.App
{
    public class Program
    {
        static void Main()
        {
            var body = "<h1>Hello World</h1>";

            var response = new HttpResponse(HttpResponseStatusCode.Ok);
            response.AddHeader(new HttpHeader("Host", "localhost:12345"));
            response.AddHeader(new HttpHeader("Date", DateTime.Now.ToString(CultureInfo.InvariantCulture)));
            response.Content = Encoding.UTF8.GetBytes(body);

            Console.WriteLine(Encoding.UTF8.GetString(response.GetBytes()));
        }
    }
}
