using System.Net.Sockets;
using System.Text;

namespace Client.Chat;

public class ChatClient
{
    private const string _host = "127.0.0.1";
    private const int _port = 8888;

    private TcpClient _tcpClient;
    private NetworkStream _stream;

    public void Start()
    {
        _tcpClient = new TcpClient();

        try
        {
            _tcpClient.Connect(_host, _port);
            _stream = _tcpClient.GetStream();

            var receiveTread = new Thread(new ThreadStart(ReceiveMessage));
            receiveTread.Start();

            SendMessage();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Disconnect();
        }
    }

    private void ReceiveMessage()
    {
        try
        {
            while (true)
            {

                var data = new byte[64];
                var sb = new StringBuilder();
                var bytes = 0;

                do
                {
                    bytes = _stream.Read(data, 0, data.Length);
                    sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (_stream.DataAvailable);

                Console.WriteLine(sb.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection interrupted: {ex.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

    private void SendMessage()
    {
        Console.WriteLine("Your message:");

        while (true)
        {
            var message = Console.ReadLine();
            var data = Encoding.Unicode.GetBytes(message);
            _stream.Write(data, 0, data.Length);
        }
    }

    private void Disconnect()
    {
        _tcpClient?.Dispose();
    }
}
