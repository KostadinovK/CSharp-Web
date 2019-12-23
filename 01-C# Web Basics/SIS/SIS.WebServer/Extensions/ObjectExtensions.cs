using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SIS.MvcFramework.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToXml(this object obj)
        {
            var serializer = new XmlSerializer(obj.GetType());

            using var stringWriter = new StringWriter();

            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings(){
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
