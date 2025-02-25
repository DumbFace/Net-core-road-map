using Microsoft.AspNetCore.Mvc;
using NetCoreAPI_Mongodb.Controllers.BaseController;
using NetCoreAPI_Mongodb.TempService;

namespace NetCoreAPI_Mongodb.Controllers.ApiVersion.v4
{
    public class NotifyController : BaseController_v4
    {
        //private readonly IHubContext<ChatHub> _chatHub;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IChatHubService _chatHubService;
        public NotifyController(IChatHubService chatHubService, IHttpContextAccessor contextAccessor)
        {
            _chatHubService = chatHubService;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string groupName, string message)
        {
            await _chatHubService.SendNotifyToGroup(groupName, message);

            //await _chatHub.SendNotifyToGroup(groupName, message);
            return Ok();
        }
    }
}
