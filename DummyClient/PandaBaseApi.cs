using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PandaLibrary;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using WebSocket4Net;


namespace DummyClient
{
    public class PandaBaseApi
    {
        private WebSocket webSocket;
        public bool IsConnected;       
         
        public async Task<bool> ConnectToServer(string host, int port)
        {
            Colorful.Console.WriteLine("Try to connect");
            
            webSocket = new WebSocket(host+port);
            webSocket.MessageReceived += Ws_MessageReceived;
            webSocket.Opened += Ws_Opened;
            webSocket.Closed += Ws_Closed;
            webSocket.DataReceived += Ws_DataReceived;
            webSocket.Error += Ws_Error;

            return await webSocket.OpenAsync();
        }

        private void Ws_Opened(object sender, EventArgs e)
        {
            IsConnected = true;           
            Colorful.Console.WriteLine("Opened", Color.Green);
        }


        private void Ws_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Colorful.Console.WriteLine($"error: {e.Exception.ToString()}", Color.Red);
        }

        private void Ws_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Colorful.Console.WriteLine("data received", Color.Tomato);
        }

        public event EventHandler OnClose;

        private void Ws_Closed(object sender, EventArgs e)
        {
            OnClose?.Invoke(this, e);
        }    

        public event EventHandler<IBaseMessage> OnMessageReceived;

        private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var json = JObject.Parse(e.Message);
            string typeName = (string)json["TypeName"];

            Type type = BaseMessageTypes.GetBaseMessageTypes().FirstOrDefault(x => x.Name.Equals(typeName));

            var request = JsonConvert.DeserializeObject(e.Message, type);

            OnMessageReceived?.Invoke(this, (IBaseMessage)request);
        }     
           
        public void SendMessage(IBaseMessage request)
        {

            var json = JsonConvert.SerializeObject(request);
            webSocket.Send(json);
        }
    }
}
