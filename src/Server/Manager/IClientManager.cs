using System.Net.Sockets;

namespace Server.Manager;
public interface IClientManager
{
    void AddClient(TcpClient tcpClient);
    void RemoveClient(string id);
    IReadOnlyCollection<Client> Clients { get; }
    void DisconnectAllClients();
}
