/*using System.Net;
using System.Net.Sockets;
using System.Text;

try
{
    const int Port = 8005;
    var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);

    var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    socket.Connect(ipPoint);

    Console.Write("Enter your message: ");
    var message = Console.ReadLine();

    var data = Encoding.Unicode.GetBytes(message is null ? string.Empty : message);
    socket.Send(data);

    data = new byte[256];
    var sb = new StringBuilder();
    var bytes = 0;

    do
    {
        bytes = socket.Receive(data, data.Length, 0);
        sb.Append(Encoding.Unicode.GetString(data, 0, bytes));
    }
    while (socket.Available > 0);

    Console.WriteLine($"Server's response: {sb}");

    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.Read();*/

using Client.Chat;

var chatClient = new ChatClient();

chatClient.Start();
