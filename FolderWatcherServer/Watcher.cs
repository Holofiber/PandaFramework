using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DummyServer;
using Newtonsoft.Json.Linq;
using FolderWatcher.Domain;

namespace FolderWatcherServer
{
    public class Watcher 
    {
        public Dictionary<Guid, string> SubscribedCatalog = new Dictionary<Guid, string>();
        FileSystemWatcher watcher = new FileSystemWatcher();
        StartServer server = new StartServer();


        public void Start()
        {
            
           // StartServer server = new StartServer();
            server.RunServer();
            server.OnConnected += _OnConnected;
        }

        private void _OnConnected(object sender, string e)
        {
            var connection = server.clientConnectionsList.FirstOrDefault();
            if (connection != null)
            connection.OnMessage += HandleMessage;
        }

        public void HandleMessage(object sender, string message)
        {            
            Console.WriteLine(message);
            var json = JObject.Parse(message);
            string typeName = (string)json["TypeName"];

            Type type = BaseMessageTypes.GetBaseMessageTypes().FirstOrDefault(x => x.Name.Equals(typeName));

            var request = JsonConvert.DeserializeObject(message, type);
           // System.Console.WriteLine($"[{request.ID}] [{request.Command}] ", Color.Cornsilk);


            
           /* else if (request.Command == ValidCommand.WaitForFolderChange)
            {
                string path = request.Message;
                var id = request.ID;
                SubscribedCatalog.Add(id, path);




                watcher.Path = path;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                                                | NotifyFilters.FileName | NotifyFilters.DirectoryName;


                watcher.Filter = "*.txt";
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Renamed += new RenamedEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnCreated);
                watcher.Deleted += new FileSystemEventHandler(OnCreated);
                watcher.EnableRaisingEvents = true;

            }
            else if (request.Command == ValidCommand.Division)
            {
                try
                {
                    var tokens = request.Message.Split();

                    var a = tokens[1];
                    var b = tokens[2];
                    Int32.TryParse(a, out int resA);
                    Int32.TryParse(b, out int resB);
                    var div = resA / resB;

                    Socket.Send(div.ToString());
                }
                catch (Exception e)
                {
                    Socket.Send(e.Message + "\n " + e.StackTrace);
                }
            }
            else
            {
                var r = new Request()
                {
                    Command = ValidCommand.Unknown,
                };
                SendMessage(r);
            }*/
        }

        /*private void SomeHapens(FileSystemEventArgs e)
        {
            List<Guid> listOfGuids = new List<Guid>();
            foreach (var data in SubscribedCatalog)
            {
                if (data.Value == Path.GetDirectoryName(e.FullPath))
                {
                    listOfGuids.Add(data.Key);
                }
            }

            if (listOfGuids.Any())
            {


                foreach (var guid in listOfGuids)
                {
                    FileSystemEvent fileSystemEvent = new FileSystemEvent
                    {
                        FileName = e.Name,
                        FullPath = e.FullPath,
                        ChangesType = e.ChangeType
                    };



                    Socket.Send(JsonConvert.SerializeObject(fileSystemEvent));
                }
            }

        }*/

        //private void OnCreated(object sender, FileSystemEventArgs e)
        //{
        //    SomeHapens(e);
        //}

        //private void OnChanged(object sender, FileSystemEventArgs e)
        //{
        //    SomeHapens(e);
        //}
    }
}
