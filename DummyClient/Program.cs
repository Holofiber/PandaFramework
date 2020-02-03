using System;
using System.Threading.Tasks;
using WebSocket4Net;

namespace DummyClient
{
    class Program
    {
        public static WebSocket ws;

        public static void Main(string[] args)
        {
            Run();
        }
        static async Task Run()
        {
            ConsoleHelper.SetWindowPosition(0, 510, 979, 512);
            Console.Title = "Client";


            Console.WriteLine("Press any key to connect");
            Console.ReadKey(true);


            var task = Task.Run(() =>
            {
                return ConnectToServer();
            });


            try
            {
                task.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            while (true)
            {
                var mesg = Console.ReadLine();
                ws.Send(mesg);
                
            }


            Console.ReadKey(true);
        }

        private static async Task ConnectToServer()
        {
            Console.WriteLine("Try to connect");
            ws = new WebSocket("ws://127.0.0.1:8181");
           // ws = new WebSocket("wss://stream.binance.com/stream?streams=btcusdt@kline_1h");

            ws.MessageReceived += Ws_MessageReceived;
            ws.Opened += Ws_Opened;
            ws.Closed += Ws_Closed;
            ws.DataReceived += Ws_DataReceived;
            ws.Error += Ws_Error;

            await ws.OpenAsync();
            
          //  ws.Send("hello");
            Console.WriteLine("message was sent");
        }

        private static void Ws_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Opened");
        }


        private static void Ws_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Console.WriteLine($"error: {e.Exception.ToString()}");
            
            
        }

        private static void Ws_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("data received");
            
        }

        private static void Ws_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("closed");
        }

        private static void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Console.WriteLine("Server said: " + e.Message);
        }
    }
}

/*
 Reconnection
 try to connection
 independent server
 reconnect client after restarting server
 
  */
 
