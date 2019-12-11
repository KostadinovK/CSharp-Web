using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public string Url { get; private set; }
        public string Path { get; private set; }
        public Dictionary<string, object> FormData { get; }
        public Dictionary<string, object> QueryData { get; }
        public IHttpHeaderCollection Headers { get; }
        public HttpRequestMethod RequestMethod { get; private set; }

        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            FormData = new Dictionary<string, object>();
            QueryData = new Dictionary<string, object>();
            Headers = new HttpHeaderCollection();

            ParseRequest(requestString);
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestString =
                requestString.Split(new[] {GlobalConstants.HttpNewLine}, StringSplitOptions.None);

            string[] requestLine =
                splitRequestString[0].Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);

            if (!IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            ParseRequestMethod(requestLine);
            ParseRequestUrl(requestLine);
            ParseRequestPath();

            ParseRequestHeaders(ParsePlainHeaders(splitRequestString).ToArray());

            ParseRequestParameters(splitRequestString[splitRequestString.Length - 1]);
        }

        

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length != 3 || requestLine[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            var methodStr = requestLine[0];

            HttpRequestMethod method;

            var parsed = HttpRequestMethod.TryParse(methodStr, true, out method);

            if (!parsed)
            {
                throw new BadRequestException();
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            Url = requestLine[1].Split('#')[0];
        }

        private void ParseRequestPath()
        {
            Path = Url.Split(new[] {'?'}, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private IEnumerable<string> ParsePlainHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!String.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequestHeaders(string[] requestLine)
        {
            foreach (var line in requestLine)
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                var kvp = line.Split(new[] {": "}, StringSplitOptions.RemoveEmptyEntries);

                var header = new HttpHeader(kvp[0], kvp[1]);
                Headers.AddHeader(header);
            }
        }

        private void ParseRequestParameters(string formData)
        {
            ParseQueryParameters();
            ParseFormDataParameters(formData);
        }

        private void ParseQueryParameters()
        {
            if (Url.Split('?').Length < 2)
            {
                return;
            }

            var parameters = Url.Split('?')[1].Split('&').ToList();

            foreach (var parameter in parameters)
            {
                var key = parameter.Split('=')[0];
                var value = parameter.Split('=')[1];

                if (!QueryData.ContainsKey(key))
                {
                    QueryData[key] = value;
                }
                else
                {
                    if (QueryData[key].GetType() == typeof(List<object>))
                    {
                        var l = new List<object>();
                        l.AddRange((List<object>)QueryData[key]);

                        l.Add(value);
                        QueryData[key] = l;
                    }
                    else
                    {
                        var prevValue = QueryData[key];

                        var list = new List<object>();
                        list.Add(prevValue);
                        list.Add(value);

                        QueryData[key] = list;
                    }
                }
            }

        }

        private void ParseFormDataParameters(string formDataStr)
        {
            if (String.IsNullOrEmpty(formDataStr))
            {
                return;
            }

            var parameters = formDataStr.Split('&').ToList();

            foreach (var parameter in parameters)
            {
                var key = parameter.Split('=')[0];
                var value = parameter.Split('=')[1];

                if (!FormData.ContainsKey(key))
                {
                    FormData[key] = value;
                }
                else
                {
                    if (FormData[key].GetType() == typeof(List<object>))
                    {
                        var l = new List<object>();
                        l.AddRange((List<object>)FormData[key]);

                        l.Add(value);
                        FormData[key] = l;
                    }
                    else
                    {
                        var prevValue = FormData[key];

                        var list = new List<object>();
                        list.Add(prevValue);
                        list.Add(value);

                        FormData[key] = list;
                    }
                }
            }
        }

    }
}
