using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace DummyClient
{
    internal class Program
    {
        private static readonly PandaBaseApi api = new PandaBaseApi();


        public static void Main(string[] args)
        {
            ConsoleHelper.SetWindowPosition(0, 510, 979, 512);
            Console.Title = "Client";

            Run();

            while (true) Thread.Sleep(1000);
        }

        private static void Run()
        {           

            var task = Task.Factory.StartNew(() => RunCommandLoop(), TaskCreationOptions.LongRunning);
        }       

        private static void RunCommandLoop()
        {
            try
            {
                while (true)
                {
                    var command = System.Console.ReadLine();

                    //if (command == "time")
                    //{
                    //    api.SendServerTimeRequest();
                    //    continue;
                    //}

                    //if (command == "ping")
                    //{
                    //    api.SendServerPing();
                    //    continue;
                    //}

                    //if (command.StartsWith("subscribe") )
                    //{
                    //    var tokens = command.Split(" ");
                    //    api.SubscribeFolderChange(tokens[1]);                        
                    //}
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
               
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