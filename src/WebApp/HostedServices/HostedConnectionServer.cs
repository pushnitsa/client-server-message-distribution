namespace WebApp.HostedServices;

public class HostedConnectionServer : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
