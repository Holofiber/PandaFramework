using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Fleck;
using Console = Colorful.Console;

namespace DummyClient
{
    class ClientConnection
    {
        public IWebSocketConnection Socket { get; }

        public ClientConnection(IWebSocketConnection socket)
        {
            Socket = socket;
            socket.OnOpen = () => Console.WriteLine("Open!");
            socket.OnClose = () => Console.WriteLine("Close!");
            socket.OnMessage = message => HandleMessage(message);

            socket.OnError = ex => Console.WriteLine(ex);
        }

        private void HandleMessage(string message)
        {
            Console.WriteLine($"From client msg: {message}", color:Color.Green);

            switch (message)
            {
                case "time":
                    Socket.Send($"Server time: {DateTime.Now}");
                    break;
                case "ping":
                    Socket.Send("pong");
                    break;
                default:
                    Socket.Send("Unknown message");
                    break;
            }
        }
    }
}
