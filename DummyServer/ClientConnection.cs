using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DummyServer;
using Fleck;
using Newtonsoft.Json;
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
            var request = JsonConvert.DeserializeObject<Request>(message);
            System.Console.WriteLine($"[{request.ID}] [{request.Command}] ", Color.Cornsilk);

            if (request.Command == ValidCommand.ServerTime)
            {
                var r = new Request()
                {
                    Command = ValidCommand.ServerTime,
                    Message = $"Server time: {DateTime.Now}"
                };
                SendMessage(r);
            }
            else if (request.Command == ValidCommand.Ping)
            {
                var r = new Request()
                {
                    Command = ValidCommand.Pong,
                    Message = "pong"
                };
                SendMessage(r);
            }
            else if (request.Command == ValidCommand.Division)
            {
                try
                {
                    var tokens = request.Message.Split();

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
            else 
            {
                var r = new Request()
                {
                    Command = ValidCommand.Uncknown,
                };
                SendMessage(r);
            }
        }

        private int i = 0;
        private void SendMessage(Request request)
        {
            request.ID = i++;
            var json = JsonConvert.SerializeObject(request);
            Socket.Send(json);
        }
    }
}
