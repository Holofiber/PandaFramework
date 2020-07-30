using DummyClient;
using Library;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebSocket4Net;
using Console = Colorful.Console;
using FolderWatcherClient.Requests;
using FolderWatcher.Domain;

namespace FolderWatcher
{
    class FolderWatcherApi 
    {       

        static Dictionary<Guid, TaskCompletionSource<FileSystemEvent>> _waitForResp =
               new Dictionary<Guid, TaskCompletionSource<FileSystemEvent>>();

        PandaBaseApi PandaApi;
        private static string Host = "ws://127.0.0.1:";
        private static int Port = 8181;

        public FolderWatcherApi()
        {
            PandaApi = new PandaBaseApi();
            PandaApi.OnMessageReceived += _OnMessageRecieved;
            PandaApi.OnClose += _OnClose;
        }

        private void _OnClose(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {

           /* var response = JsonConvert.DeserializeObject<Request>(e.Message);            

            if (response.Command == ValidCommand.FolderChanged)
            {
                FileSystemEvent res = JsonConvert.DeserializeObject<FileSystemEvent>(response.Object.ToString());
                DoOnFolderChanged(res);
            }
            else
            {
                DoShowMessage(e.Message);
            }*/
        }

        internal void ConnectToServer()
        {            
           Task task = PandaApi.ConnectToServer(Host, Port);
        }

        private void _OnMessageRecieved(object sender, IBaseMessage e)
        {
            throw new NotImplementedException();
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
            PandaApi.SendMessage(request);            

            return tcs.Task;
        }
        //Unsubscribe
        //OnFolederChange 
        public event EventHandler<FolderChangesResponse> OnFolderChanged;

        protected virtual void DoOnFolderChanged(FolderChangesResponse e)
        {
            OnFolderChanged?.Invoke(this, e);
        }

        
    }
}
