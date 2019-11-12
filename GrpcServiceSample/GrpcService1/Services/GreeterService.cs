using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService1
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task SayHelloStrem(HelloRequest request, IServerStreamWriter<HelloReply> responseStream, ServerCallContext context)
        {
            await responseStream.WriteAsync(new HelloReply { Message = "Hi" });
            await Task.Delay(500);
            await responseStream.WriteAsync(new HelloReply { Message = "I'm streaming now" });
            await Task.Delay(1000);
            await responseStream.WriteAsync(new HelloReply { Message = "Bye bye" });
        }
    }
}
