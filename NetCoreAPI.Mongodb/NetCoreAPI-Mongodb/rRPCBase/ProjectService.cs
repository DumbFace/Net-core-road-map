using Grpc.Core;
using GrpcProject;

namespace NetCoreAPI_Mongodb.rRPCBase
{
    public class ProjectService : GrpcServiceProject.GrpcServiceProjectBase
    {
        public override Task<GetProjectsResponse> GetProjects(Empty request, ServerCallContext context)
        {
            return base.GetProjects(request, context);
        }
    }
}
