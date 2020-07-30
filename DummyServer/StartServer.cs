using System;
using System.Collections.Generic;
using DummyClient;
using Fleck;

namespace DummyServer
{
    public class StartServer
    {
        WebSocketServer server;
        public  List<ClientConnection> clientConnectionsList = new List<ClientConnection>();
        public event EventHandler<string> OnConnected;
        public void RunServer()
        {
            server = new WebSocketServer("ws://0.0.0.0:8181");
            server.Start(socket =>
            {
                var clientConnection = new ClientConnection(socket);                
                clientConnectionsList.Add(clientConnection);
                OnConnected?.Invoke(this, "new connection");
            });            
        }
    }
}
