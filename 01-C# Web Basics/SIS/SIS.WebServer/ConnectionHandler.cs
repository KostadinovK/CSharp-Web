using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;
using SIS.WebServer.Routing.Contracts;
using SIS.WebServer.Sessions;

namespace SIS.WebServer
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRoutingTable serverRoutingTable;

        public ConnectionHandler(Socket client, IServerRoutingTable serverRoutingTable)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRoutingTable, nameof(serverRoutingTable));

            this.client = client;
            this.serverRoutingTable = serverRoutingTable;
        }

        public async Task ProcessRequestAsync()
        {
            try
            {
                var httpRequest = await ReadRequestAsync();

                if (httpRequest != null)
                {
                    Console.WriteLine($"Processing: {httpRequest.RequestMethod} {httpRequest.Path}...");
                    var sessionId = SetRequestSession(httpRequest);
                    var httpResponse = HandleRequest(httpRequest);
                    SetResponseSession(httpResponse, sessionId);
                    PrepareResponse(httpResponse);
                }
            }
            catch (BadRequestException e)
            {
                PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest));
            }
            catch (Exception e)
            {
                PrepareResponse(new TextResult(e.ToString(), HttpResponseStatusCode.BadRequest));
            }

            client.Shutdown(SocketShutdown.Both);
        }

        private string SetRequestSession(IHttpRequest httpRequest)
        {
            string sessionId = null;

            if (httpRequest.Cookies.ContainsCookie(HttpSessionStorage.SessionCookieKey))
            {
                var cookie = httpRequest.Cookies.GetCookie(HttpSessionStorage.SessionCookieKey);
                sessionId = cookie.Value;
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
                httpRequest.Session = HttpSessionStorage.GetSession(sessionId);
            }

            return httpRequest.Session.Id;
        }

        private void SetResponseSession(IHttpResponse httpResponse, string sessionId)
        {
            if (sessionId != null)
            {
                httpResponse.AddCookie(new HttpCookie(HttpSessionStorage.SessionCookieKey, sessionId));
            }
        }

        private async Task<IHttpRequest> ReadRequestAsync()
        {
            var result = new StringBuilder();
            var data = new ArraySegment<byte>(new byte[1024]);

            while (true)
            {
                int numberOfBytesRead = await client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);
                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }

        private IHttpResponse HandleRequest(IHttpRequest httpRequest)
        {
            if (!serverRoutingTable.Contains(httpRequest.RequestMethod, httpRequest.Path))
            {
                return new TextResult($"Route with method {httpRequest.RequestMethod} and path \"{httpRequest.Path}\" not found.", HttpResponseStatusCode.NotFound);
            }

            return serverRoutingTable.Get(httpRequest.RequestMethod, httpRequest.Path).Invoke(httpRequest);
        }

        private void PrepareResponse(IHttpResponse httpResponse)
        {
            var byteSegments = httpResponse.GetBytes();

            client.Send(byteSegments.ToArray(), SocketFlags.None);
        }

    }
}
