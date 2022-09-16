using Microsoft.Extensions.Configuration;
using Server.Configuration;
using Server.Host;
using Server.Manager;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServerExtensions
{
    public static void AddConnectionServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionServerOptions>(configuration.GetSection(ConnectionServerOptions.ConnectionServer));
        services.AddSingleton<HostService>();
        services.AddSingleton<IClientManager, ClientManager>();
    }
}

