using Grpc.Core;
using GrpcGreeter;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreAPI_Mongodb.rRPCBase
{
    //[Authorize]
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
        }
    }
}
