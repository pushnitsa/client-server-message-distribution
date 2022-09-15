using System.Net.Sockets;
using System.Text;

namespace Server.Chat;

public class ChatClient
{
    private TcpClient _tcpClient;
    private HostService _hostService;
    public string Id { get; set; }
    public NetworkStream Stream { get; set; }

    public ChatClient(TcpClient tcpClient, HostService hostService)
    {
        Id = Guid.NewGuid().ToString();
        _tcpClient = tcpClient;
        _hostService = hostService;
        _hostService.AddConnection(this);
        Stream = tcpClient.GetStream();
    }

    public void Process()
    {
        try
        {
            while (true)
            {
                try
                {
                    var message = GetMessage();
                    Console.WriteLine($"{Id}: {message}");
                    _hostService.BroadcastMessage(message, Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{Id}: {ex.Message}");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            _hostService.RemoveConnection(Id);
            Close();
        }
    }

    private string GetMessage()
    {
        var data = new byte[64];
        var sb = new StringBuilder();
        int bytes;

        do
        {
            bytes = Stream.Read(data, 0, data.Length);
            sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        while (Stream.DataAvailable);

        return sb.ToString();
    }

    public void Close()
    {
        Stream?.Close();
    }
}
