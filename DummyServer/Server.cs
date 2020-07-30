using System;
using System.Collections.Generic;
using System.Threading;
using DummyClient;
using Fleck;


namespace DummyServer
{
    public class Server
    {
        static void Main(string[] args)
        {
            ConsoleHelper.SetWindowPosition(0, 0, 979, 512);
            Console.Title = $"Panda Server {DateTime.Now}";           

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
