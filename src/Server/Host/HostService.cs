using Microsoft.Extensions.Options;
using Server.Configuration;
using Server.Manager;
using System.Net;
using System.Net.Sockets;

namespace Server.Host;

public class HostService : IHostService
{
    private TcpListener _tcpListener;
    private readonly ConnectionServerOptions _options;
    private readonly IClientManager _clientManager;

    public HostService(
        IOptions<ConnectionServerOptions> serverOptions,
        IClientManager clientManager)
    {
        _options = serverOptions.Value;
        _clientManager = clientManager;
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
        }
    }
}
