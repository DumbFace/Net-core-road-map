namespace NetCoreAPI_Mongodb.SignalRHub
{
    public interface IChatHub
    {
        Task SendNotifyToGroup(string groupName, string message);

        Task AddToGroup(string groupName);
    }
}
