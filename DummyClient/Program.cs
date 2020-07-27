using Library;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace DummyClient
{
    internal class Program
    {
        private static readonly Api api = new Api();

        public static void Main(string[] args)
        {
            ConsoleHelper.SetWindowPosition(0, 510, 979, 512);
            Console.Title = "Client";

            Task task = Run();

            while (true) Thread.Sleep(1000);
        }

        private static async Task Run()
        {
            api.OnServerTime += ApiOnServerTime;
            api.OnShowMessage += ApiOnOnShowMessage;
            api.OnFolderChanged += ApiOnFolderChanged;
            await api.ConnectToServer();

            var task = Task.Factory.StartNew(() => RunCommandLoop(), TaskCreationOptions.LongRunning);            
        }

        private static void ApiOnFolderChanged(object sender, FileSystemEvent e)
        {
            Console.WriteLine("Folder was changed with result" + e);
           // throw new NotImplementedException();
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