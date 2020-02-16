using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using WebSocket4Net;
using Console = Colorful.Console;

namespace DummyClient
{
    class Program
    {
        private static readonly Api api = new Api();


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
            
            api.ConnectToServer();
            api.OnServerTime += ApiOnServerTime;
            api.OnShowMessage += ApiOnOnShowMessage;




            Task.Run(() => RunCommandLoop());

            while (true)
            {
                Thread.Sleep(1000);
            }

            Console.ReadKey(true);
        }

        private static void ApiOnOnShowMessage(object sender, string e)
        {
            Console.WriteLine("Unknown message from server:", Color.Red); 
            Console.Write(e, Color.AliceBlue);
        }

        private static void RunCommandLoop()
        {
            try
            {
                while (true)
                {
                    var command = System.Console.ReadLine();

                    if (command == "time")
                    {
                        api.SendServerTimeRequest();
                    }

                    else if (command.StartsWith("div"))
                    {
                        var tokens = command.Split();

                        var a = tokens[1];
                        var b = tokens[2];
                        Int32.TryParse(a, out int numberA);
                        Int32.TryParse(b, out int numberB);

                        api.DivNumbers(numberA, numberB);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
                throw;
            }
        }

        private static void ApiOnServerTime(object sender, string e)
        {
            Console.WriteLine(e, Color.Gold);
        }
    }
}

/*
 Reconnection
 try to connection
 independent server
 reconnect client after restarting server
 
  */
 
