using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiController]
    [ApiVersion(1.0)]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v1/[controller]")]
    public class BaseController_v1 : ControllerBase
    {

    }
}
