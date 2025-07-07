
using Microsoft.AspNetCore.SignalR;
using NetCoreAPI_Mongodb.SignalRHub;

namespace NetCoreAPI_Mongodb.TempService
{
    public class ChatHubService : IChatHubService
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotifyToGroup(string groupName, string message)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotify", message);
        }
    }
}
