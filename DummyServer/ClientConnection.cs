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

            if (message == "time")
            {
                Socket.Send($"Server time: {DateTime.Now}");
            }
            else if (message == "ping")
            {
                Socket.Send("pong");
            }
            else if (message.StartsWith("div"))
            {
                try
                {
                    var tokens = message.Split();

                    var a = tokens[1];
                    var b = tokens[2];
                    Int32.TryParse(a, out int resA);
                    Int32.TryParse(b, out int resB);
                    var div = resA / resB;

                    Socket.Send(div.ToString());
                }
                catch (Exception e)
                {
                    Socket.Send(e.Message +"\n "+ e.StackTrace);
                }
            }
            else if (message == default)
            {
                Socket.Send("Unknown message");
            }
        }
    }
}
