namespace Server.Host;
public interface IHaveClients
{
    IReadOnlyCollection<Client> Clients { get; }
}
