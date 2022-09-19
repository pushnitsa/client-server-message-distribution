using Microsoft.Extensions.Options;
using Server.Configuration;
using Server.Manager;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Host;

public class HostService : IHostService, IBroadcast
{
    private TcpListener _tcpListener;
    private readonly ConnectionServerOptions _options;
    private readonly IClientManager _clientManager;
    private readonly IHaveClients _clientsStorage;

    public HostService(
        IOptions<ConnectionServerOptions> serverOptions,
        IClientManager clientManager,
        IHaveClients clientsStorage
        )
    {
        _options = serverOptions.Value;
        _clientManager = clientManager;
        _clientsStorage = clientsStorage;
    }

    public async Task ListenAsync(CancellationToken cancellationToken)
    {
        try
        {
            _tcpListener = new TcpListener(IPAddress.Any, _options.ServerPort);
            _tcpListener.Start();

            while (!cancellationToken.IsCancellationRequested)
            {
                var tcpClient = await _tcpListener.AcceptTcpClientAsync(cancellationToken);

                _clientManager.AddClient(tcpClient);
            }
        }
        catch (Exception ex)
        {
            // Add logging
        }
        finally
        {
            _tcpListener.Stop();
            _clientManager.DisconnectAllClients();
        }
    }

    public async Task BroadcastAsync(string message)
    {

        var data = Encoding.UTF8.GetBytes(message);

        foreach (var client in _clientsStorage.Clients)
        {
            if (!client.TcpClient.Connected)
            {
                _clientManager.RemoveClient(client.Id);
                continue;
            }

            await client.Stream.WriteAsync(data);
        }
    }
}
