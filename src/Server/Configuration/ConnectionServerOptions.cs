namespace Server.Configuration;
public class ConnectionServerOptions
{
    public const string ConnectionServer = "ConnectionService";

    public int ServerPort { get; set; } = 8888;
}
