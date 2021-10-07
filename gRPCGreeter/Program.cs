using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Grpc.Net.Client;

namespace gRPCGreeter
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = "/Users/kuwabara_yuki/workspace/git/dolow/unity_grpc_build_sample/grpc_server/cmd/test_client";
            string certPath = System.IO.Path.Combine(dir, "192.168.11.9.pem");

            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificates.Add(new X509Certificate2(certPath));

            using HttpClient httpClient = new HttpClient(handler);
            GrpcChannel channel = GrpcChannel.ForAddress("https://192.168.11.9:50051", new GrpcChannelOptions
            {
                HttpClient = httpClient
            });

            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            Helloworld.Greeter.GreeterClient client = new Helloworld.Greeter.GreeterClient(channel);

            Helloworld.HelloRequest req = new Helloworld.HelloRequest();
            req.Name = "Pure C# World";
            Helloworld.HelloReply rep = client.SayHello(req);
            Console.WriteLine(rep.Message.ToString());
        }
    }
}
