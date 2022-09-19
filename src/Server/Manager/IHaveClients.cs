namespace Server.Manager;
public interface IHaveClients
{
    IReadOnlyCollection<Client> Clients { get; }
}
