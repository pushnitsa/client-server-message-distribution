namespace Server.Host;

public interface IBroadcast
{
    Task BroadcastAsync(string message);
}
