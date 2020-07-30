using System;
using System.Collections.Generic;
using Fleck;
using Console = Colorful.Console;

namespace DummyClient
{
    public class ClientConnection
    {
        public IWebSocketConnection Socket { get; }
        public Dictionary<Guid, string> SubscribedCatalog = new Dictionary<Guid, string>();       

        public ClientConnection(IWebSocketConnection socket)
        {
            Socket = socket;
            socket.OnOpen = () => Console.WriteLine("Open new connection!");
            socket.OnClose = () => Console.WriteLine("Conncetion closed!");
            socket.OnMessage = message => HandleMessage(message);

            socket.OnError = ex => HandleError(ex);
        }       

        public event EventHandler<string> OnMessage;
        public event EventHandler<Exception> OnError;       
       
        private void HandleError(Exception exception)
        {
            OnError?.Invoke(this, exception);
        }

        public void SendMessage(string message)
        {
            Socket.Send(message);
        }

        private void HandleMessage(string message)
        { 
            OnMessage?.Invoke(this, message);       
        }    
    }
}
