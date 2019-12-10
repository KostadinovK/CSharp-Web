using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Demo
{
    public class Program
    {
        public static void Main()
        {
            var tcpListener = new TcpListener(IPAddress.Loopback, 12345);
            tcpListener.Start();

            while (true)
            {
                var tcpClient = tcpListener.AcceptTcpClient();
                using NetworkStream stream = tcpClient.GetStream();
                byte[] bytes = new byte[10000];

                int readBytes = stream.Read(bytes, 0, bytes.Length);
                var request = Encoding.UTF8.GetString(bytes, 0, readBytes);
                Console.WriteLine(request);

                var responseBody = File.ReadAllText("../../../index.html");

                var response = "HTTP/1.0 200 OK" + Environment.NewLine +
                                  "Content-Type: text/html" + Environment.NewLine +
                                  "Server: MyCustomServer/1.0" + Environment.NewLine +
                                  $"Content-Length: {responseBody.Length}" + Environment.NewLine + Environment.NewLine +
                                  responseBody;

                var responseBytes = Encoding.UTF8.GetBytes(response);
                stream.Write(responseBytes, 0 , responseBytes.Length);
            }
        }
    }
}
