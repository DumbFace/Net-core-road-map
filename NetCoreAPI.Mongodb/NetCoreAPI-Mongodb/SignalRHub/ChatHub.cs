using Microsoft.AspNetCore.SignalR;

namespace NetCoreAPI_Mongodb.SignalRHub
{
    public class ChatHub : Hub
    //, IChatHub
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ChatHub(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task SendNotifyToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveNotify", message);
        }
        public override async Task OnConnectedAsync()
        {
            var storeID = Context.GetHttpContext().Request.Query["storeId"];
            System.Diagnostics.Debug.WriteLine($"storeID: {storeID}");
            await Groups.AddToGroupAsync(Context.ConnectionId, storeID);
        }
    }
}