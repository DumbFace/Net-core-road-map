using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreAPI_Mongodb.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected private IMediator _mediator;
        public BaseController()
        {
        }

        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
