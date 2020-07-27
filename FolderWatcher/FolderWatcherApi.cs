using DummyClient;
using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocket4Net;
using System.Drawing;
using Console = Colorful.Console;
using FolderWatcherClient.Requests;

namespace FolderWatcher
{
    class FolderWatcherApi 
    {       

        static Dictionary<Guid, TaskCompletionSource<FileSystemEvent>> _waitForResp =
               new Dictionary<Guid, TaskCompletionSource<FileSystemEvent>>();

        Api pandaApi = new Api();
        private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {

            var response = JsonConvert.DeserializeObject<Request>(e.Message);            

            if (response.Command == ValidCommand.FolderChanged)
            {
                FileSystemEvent res = JsonConvert.DeserializeObject<FileSystemEvent>(response.Object.ToString());
                DoOnFolderChanged(res);
            }
            else
            {
                DoShowMessage(e.Message);
            }
        }

        internal void ConnectToServer()
        {
           Task task = pandaApi.ConnectToServer();
        }

        private void DoShowMessage(object message)
        {
            Console.WriteLine(message);
        }

        //Subscribe
        public Task<FileSystemEvent> SubscribeFolderChange(string path, Guid id)
        {
            var request = new SubscribeRequest()
            {
                Path =  $@"C:\{path}",
                ID = id
            };
            


            var tcs = new TaskCompletionSource<FileSystemEvent>();
            System.Console.WriteLine(request.ID);
            _waitForResp.Add(request.ID, tcs);
            pandaApi.SendMessage(request);

            return tcs.Task;
        }
        //Unsubscribe
        //OnFolederChange 
        public event EventHandler<FileSystemEvent> OnFolderChanged;

        protected virtual void DoOnFolderChanged(FileSystemEvent e)
        {
            OnFolderChanged?.Invoke(this, e);
        }

        public void FolderChanged(Request r)
        {
            Colorful.Console.WriteLine($"{r.ID} {r.Message}", Color.Green);
        }
    }
}
