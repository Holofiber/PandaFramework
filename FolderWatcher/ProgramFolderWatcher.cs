using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DummyClient;
using Library;

namespace FolderWatcher
{
    class ProgramFolderWatcher
    {
        private static Guid userID;
        static FolderWatcherApi FWApi = new FolderWatcherApi();       

        static void Main(string[] args)        {

            Run();
            while (true) Thread.Sleep(1000);
        }

        private static void Run()
        {
            userID = Guid.NewGuid();
            FWApi.OnFolderChanged += _OnFolderChanged;
            FWApi.ConnectToServer();

           
           // Task<FileSystemEvent> task = SubscribeFolderChange(folderPath);

            var commandLoopTask = Task.Factory.StartNew(() => RunCommandLoop(), TaskCreationOptions.LongRunning);
        }

        private static void RunCommandLoop()
        {
            try
            {
                while (true)
                {
                    var command = Console.ReadLine();                    

                    if (command.StartsWith("subscribe"))
                    {
                        var tokens = command.Split(" ");
                        FWApi.SubscribeFolderChange(tokens[1], userID);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }

        private static void _OnFolderChanged(object sender, FileSystemEvent e)
        {
            Console.WriteLine("Folder was changed with result" + e);
        }

        static Dictionary<Guid, TaskCompletionSource<FileSystemEvent>> _waitForResponse = new Dictionary<Guid, TaskCompletionSource<FileSystemEvent>>();
        

       
    }
}
