using System.Net.Sockets;
using System.Text;

namespace Client.Chat;

public class ChatClient
{
    private string _host;
    private int _port;

    public ChatClient(string? host, int? port)
    {
        _host = host ?? "127.0.0.1";
        _port = port == null ? 8885 : (int)port;
    }

    public void Start()
    {
        try
        {
            var receiveTread1 = new Thread(new ThreadStart(ReceiveMessage));
            receiveTread1.Start();

            var receiveTread2 = new Thread(new ThreadStart(ReceiveMessage));
            receiveTread2.Start();

            var receiveTread3 = new Thread(new ThreadStart(ReceiveMessage));
            receiveTread3.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void ReceiveMessage()
    {
        var tcpClient = new TcpClient();

        try
        {
            tcpClient.Connect(_host, _port);
            var stream = tcpClient.GetStream();

            while (true)
            {

                var data = new byte[64];
                var sb = new StringBuilder();
                var bytes = 0;

                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    sb.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable);

                Console.WriteLine(sb.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection interrupted: {ex.Message}");
        }
        finally
        {
            tcpClient?.Dispose();
        }
    }
}
