using Microsoft.Extensions.Configuration;
using Server.Configuration;
using Server.Host;
using Server.Manager;
using Server.Models;
using System.Net.Sockets;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServerExtensions
{
    public static void AddConnectionServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionServerOptions>(configuration.GetSection(ConnectionServerOptions.ConnectionServer));

        services.AddSingleton<HostService>();
        services.AddSingleton<IHostService>(p => p.GetRequiredService<HostService>());
        services.AddSingleton<IBroadcast>(p => p.GetRequiredService<HostService>());

        services.AddSingleton<ClientManager>();
        services.AddSingleton<IHaveClients>(p => p.GetRequiredService<ClientManager>());
        services.AddSingleton<IClientManager>(p => p.GetRequiredService<ClientManager>());

        services.AddSingleton<Func<TcpClient, Client>>((tcpClient) => new Client(tcpClient));
    }
}
