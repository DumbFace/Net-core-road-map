using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiVersion(4.0)]
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/v4/[controller]")]
    public class BaseController_v4 : BaseController
    {

        public BaseController_v4()
        {
        }

        public BaseController_v4(IMediator mediator) : base(mediator)
        {
        }
    }
}
