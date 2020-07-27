using System;
using System.Threading;

namespace FolderWatcherServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = $"Watcher Server {DateTime.Now}";
            Console.WriteLine("Start");
            Watcher watcher = new Watcher();
            watcher.Start();
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
