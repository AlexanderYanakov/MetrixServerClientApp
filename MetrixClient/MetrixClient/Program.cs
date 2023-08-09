using Domain;
using Metrixclient.Impl;
using Microsoft.AspNetCore.SignalR.Client;
using Service.Impl;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MetrixClient
{
    public class Program
    {
        private static readonly string Url = "http://localhost:6060/MetrixHub"; //url to connect to server
        public static int Time = 3; //default time is 3 seconds
        public static async Task Main(string[] args)


        {
            HubConnection connection = ConnectToServer(Url);


            await ListenerMessages(connection);

            while (true)
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {


                    WinMetrixService winMetrixService = new WinMetrixService();
                    MetrixInfo metrixInfo = winMetrixService.GetMetrix();
                    if (connection != null)
                    {
                        await SendMessageToServer(connection, metrixInfo);
                    }
                    Thread.Sleep(Time * 1000);
                }

                //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                //{
                //    LinMetrixService linMetrixService = new LinMetrixService();
                //    MetrixInfo metrixInfo = linMetrixService.GetMetrix();
                //    if (connection != null)
                //    {
                //        await SendMessageToServer(connection, metrixInfo);
                //    }
                //    Thread.Sleep(Time * 1000);
                //}
            }
        }

        private async static Task ListenerMessages(HubConnection connection)
        {
            connection.On<int>("ReceiveMessage", (message) =>
            {
                Console.WriteLine(message);
                Time = message;
            });
        }

        private static HubConnection ConnectToServer(string url)
        {
            var connection = new HubConnectionBuilder()
            .WithUrl(url)
            .Build();

            connection.StartAsync().Wait();

            return connection;
        }
        
        private static async Task SendMessageToServer(HubConnection connection, object message)
        {
            string metrixJson = JsonSerializer.Serialize(message);
            await connection.InvokeCoreAsync("SaveMetrix", args: new[] { metrixJson });
        }
    }
}
