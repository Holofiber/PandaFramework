using System;
using System.Threading;
using Fleck;


namespace DummyServer
{
    class Program
    {

        static WebSocketServer server;

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
                socket.OnOpen = () => Console.WriteLine("Open!");
                socket.OnClose = () => Console.WriteLine("Close!");
                socket.OnMessage = message =>
                {
                    Console.WriteLine($"From client msg: {message}");
                    socket.Send($"Reply: {message}");
                };
                socket.OnError = ex => Console.WriteLine(ex);
                
            });


            
        }
    }
}
