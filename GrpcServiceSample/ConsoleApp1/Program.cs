using Grpc.Core;
using Grpc.Net.Client;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress(("https://localhost:5001"));
            var client = new GrpcService1.Greeter.GreeterClient(channel);

            var response = client.SayHello(new GrpcService1.HelloRequest() { Name = "Ivan" });

            Console.WriteLine(response.Message);
            Console.ReadLine();

            using (var statusReplies = client.SayHelloStrem(new GrpcService1.HelloRequest() { Name = "Ciao a tutti" }))
            {
                while (await statusReplies.ResponseStream.MoveNext())
                {
                    var replyStream = statusReplies.ResponseStream.Current.Message;
                    Console.WriteLine($"{replyStream}");
                }
            }

            Console.ReadLine();
        }
    }
}
