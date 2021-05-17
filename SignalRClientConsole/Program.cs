using System;
using System.Net.Http;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://127.0.0.1:5001/chatHub", (opts) =>
                {
                    opts.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)

                            // Usando o certificado de desenvolvedor auto assinado do .NET Core
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
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
