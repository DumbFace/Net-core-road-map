using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiVersion(5.0)]
    [ApiExplorerSettings(GroupName = "v5")]
    [Route("api/v5/[controller]")]
    public class BaseController_v5 : BaseController
    {

        public BaseController_v5()
        {
        }

        public BaseController_v5(IMediator mediator) : base(mediator)
        {
        }
    }
}
