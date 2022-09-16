namespace Server.Host;

public interface IHostService
{
    Task ListenAsync(CancellationToken cancellationToken);
}
