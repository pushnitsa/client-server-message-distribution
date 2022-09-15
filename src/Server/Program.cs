
/*const int Port = 8005;
var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

try
{
    listenSocket.Bind(ipPoint);
    listenSocket.Listen(10);

    Console.WriteLine("Server started. Now listening...");

    while (true)
    {
        var handler = listenSocket.Accept();

        var sb = new StringBuilder();
        var bytes = 0;
        var data = new byte[256];

        do
        {
            bytes = handler.Receive(data);
            sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        while (handler.Available > 0);

        Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {sb.ToString()}");

        const string response = "Your message delivered";

        data = Encoding.Unicode.GetBytes(response);

        handler.Send(data);

        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}*/

using Server.Chat;

var server = new HostService();

server.Listen();
