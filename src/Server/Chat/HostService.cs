using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.Chat;
public class HostService
{
    private TcpListener _tcpListener;
    private readonly List<ChatClient> _clients = new List<ChatClient>();

    protected internal void AddConnection(ChatClient chatClient)
    {
        _clients.Add(chatClient);
    }

    protected internal void RemoveConnection(string id)
    {
        var client = _clients.FirstOrDefault(x => x.Id == id);

        if (client != null)
        {
            _clients.Remove(client);
        }
    }

    protected internal void Listen()
    {
        try
        {
            _tcpListener = new TcpListener(IPAddress.Any, 8888);
            _tcpListener.Start();
            Console.WriteLine("Server start");

            while (true)
            {
                var tcpClient = _tcpListener.AcceptTcpClient();

                var chatClient = new ChatClient(tcpClient, this);

                var clientThread = new Thread(new ThreadStart(chatClient.Process));
                clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Disconnect();
        }
    }

    protected internal void BroadcastMessage(string message, string id)
    {
        var data = Encoding.Unicode.GetBytes(message);

        foreach (var client in _clients)
        {
            if (client.Id != id)
            {
                client.Stream.Write(data, 0, data.Length);
            }
        }
    }

    private void Disconnect()
    {
        _tcpListener.Stop();

        foreach (var client in _clients)
        {
            client.Close();
        }
    }
}
