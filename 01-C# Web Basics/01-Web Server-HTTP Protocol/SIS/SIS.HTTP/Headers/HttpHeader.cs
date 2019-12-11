using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Common;

namespace SIS.HTTP.Headers
{
    public class HttpHeader
    {
        public string Key { get; }

        public string Value { get; }

        public HttpHeader(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
