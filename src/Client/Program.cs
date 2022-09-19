using Client.Chat;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

int.TryParse(configuration["Port"], out var port);
var host = configuration["Host"];

var chatClient = new ChatClient(host, port);

chatClient.Start();

Console.ReadLine();
