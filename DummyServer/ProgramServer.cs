using System;
using System.Collections.Generic;
using System.Threading;
using DummyClient;
using Fleck;


namespace DummyServer
{
    class Program
    {

        static WebSocketServer server;
        private static List<ClientConnection> clientConnectionsList = new List<ClientConnection>();

        static void Main(string[] args)
        {
            ConsoleHelper.SetWindowPosition(0, 0, 979, 512);

            StartServer();

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        public static  void StartServer()
        {

            server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                var clientConnection = new ClientConnection(socket);
                clientConnectionsList.Add(clientConnection);
            });            
        }

        
    }
}
