using System.Net.Sockets;

namespace Server.Manager;
public interface IClientManager
{
    void AddClient(TcpClient tcpClient);
    void RemoveClient(string id);
    void DisconnectAllClients();
}
