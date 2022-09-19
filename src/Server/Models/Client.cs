using System.Net.Sockets;

namespace Server.Models;

public class Client
{
    public TcpClient TcpClient;
    public string Id { get; set; }
    public NetworkStream Stream { get; set; }

    public Client(TcpClient tcpClient)
    {
        Id = Guid.NewGuid().ToString();
        TcpClient = tcpClient;
        Stream = tcpClient.GetStream();
    }

}
