using Server.Host;

namespace WebApp.HostedServices;

public class HostedConnectionServer : BackgroundService
{
    private readonly IHostService _hostService;

    public HostedConnectionServer(IHostService hostService)
    {
        _hostService = hostService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _hostService.ListenAsync(stoppingToken);
    }
}
