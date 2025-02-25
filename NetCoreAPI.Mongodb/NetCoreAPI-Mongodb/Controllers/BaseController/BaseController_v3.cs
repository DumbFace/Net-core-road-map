using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiController]
    [ApiVersion(3.0)]
    [ApiExplorerSettings(GroupName = "v3")]
    [Route("api/v3/[controller]")]
    public class BaseController_v3 : ControllerBase
    {

    }
}
