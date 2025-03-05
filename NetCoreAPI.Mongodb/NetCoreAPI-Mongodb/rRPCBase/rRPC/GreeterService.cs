using Grpc.Core;
using GrpcGreeter;

namespace NetCoreAPI_Mongodb.rRPCBase.rRPC
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = $"Hello {request.Name}" });
        }
    }
}
