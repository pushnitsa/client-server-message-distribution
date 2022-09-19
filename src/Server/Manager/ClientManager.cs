using Server.Models;
using System.Collections.Immutable;
using System.Net.Sockets;

namespace Server.Manager;
public class ClientManager : IClientManager, IHaveClients
{
    private readonly List<Client> _clients = new();
    private readonly object _lock = new();
    private readonly Func<TcpClient, Client> _clientFactory;

    public ClientManager(Func<TcpClient, Client> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public void AddClient(TcpClient tcpClient)
    {
        lock (_lock)
        {
            _clients.Add(_clientFactory(tcpClient));
        }
    }

    public IReadOnlyCollection<Client> Clients => _clients.ToImmutableList();

    public void RemoveClient(string id)
    {
        lock (_lock)
        {
            var client = _clients.FirstOrDefault(x => x.Id == id);

            if (client != null)
            {
                client.TcpClient.Close();

                _clients.Remove(client);
            }
        }
    }

    public void DisconnectAllClients()
    {
        foreach (var client in Clients)
        {
            RemoveClient(client.Id);
        }
    }
}
