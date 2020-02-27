using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace DummyClient
{
    internal class Program
    {
        private static readonly Api api = new Api();

        public static async Task Main(string[] args)
        {
           // ConsoleHelper.SetWindowPosition(0, 510, 979, 512);
            Console.Title = "Client";

            Console.WriteLine("Press any key to connect");
            Console.ReadKey(true);

            api.OnServerTime += ApiOnServerTime;
            api.OnShowMessage += ApiOnOnShowMessage;
            await api.ConnectToServer();


           await Task.Run(() => RunCommandLoop());

           // while (true) Thread.Sleep(1000);
        }

        private static void ApiOnOnShowMessage(object sender, string e)
        {
            Console.WriteLine("Unknown message from server:", Color.Red);
            Console.Write(e, Color.AliceBlue);
        }

        private static async Task RunCommandLoop()
        {
            try
            {
                while (true)
                {
                    var command = System.Console.ReadLine();

                    if (command == "time")
                    {
                        api.SendServerTimeRequest();
                        continue;
                    }


                    var changeResult=await api.WaitForFolderChange(command);

                    System.Console.WriteLine("Folder was changed with result" + changeResult);


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