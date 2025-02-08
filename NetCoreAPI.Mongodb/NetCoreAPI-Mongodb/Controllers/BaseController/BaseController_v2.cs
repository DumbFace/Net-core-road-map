using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [ApiController]
    [ApiVersion(2.0)]
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/v2/[controller]")]
    public class BaseController_v2 : ControllerBase
    {

    }
}
