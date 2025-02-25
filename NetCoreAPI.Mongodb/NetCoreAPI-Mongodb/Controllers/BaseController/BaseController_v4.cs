using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiController]
    [ApiVersion(4.0)]
    [ApiExplorerSettings(GroupName = "v4")]
    [Route("api/v4/[controller]")]
    public class BaseController_v4 : ControllerBase
    {

    }
}
