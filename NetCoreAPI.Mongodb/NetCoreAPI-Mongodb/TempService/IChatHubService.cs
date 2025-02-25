namespace NetCoreAPI_Mongodb.TempService
{
    public interface IChatHubService
    {
        Task SendNotifyToGroup(string groupName, string message);
    }
}
