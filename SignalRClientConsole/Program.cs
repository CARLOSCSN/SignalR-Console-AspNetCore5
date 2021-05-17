using System;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(new Uri("https://localhost:5001/chatHub"))
                .WithAutomaticReconnect()
                .Build();

            connection.StartAsync().Wait();
            connection.InvokeCoreAsync("SendMessage", args: new[] { "Carlos", "Teste de SignalR HTTPS in Console App" });
            connection.On("ReceiveMessage", (string userName, string message) =>
             {
                 Console.WriteLine(userName + ":" + message);
             });

            Console.ReadKey();
        }
    }
}
