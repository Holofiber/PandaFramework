using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DummyServer;
using Newtonsoft.Json;
using WebSocket4Net;
using Console = Colorful.Console;

namespace DummyClient
{
    public class Api
    {
        private WebSocket webSocket;
        public bool IsConnected;
        private Timer aTimer;
        private int i = 0;

        public Api()
        {
            webSocket = new WebSocket("ws://127.0.0.1:8181");
        }

        public async Task ConnectToServer()
        {
            Colorful.Console.WriteLine("Try to connect");
            // ws = new WebSocket("wss://stream.binance.com/stream?streams=btcusdt@kline_1h");

            webSocket.MessageReceived += Ws_MessageReceived;
            webSocket.Opened += Ws_Opened;
            webSocket.Closed += Ws_Closed;
            webSocket.DataReceived += Ws_DataReceived;
            webSocket.Error += Ws_Error;


            await webSocket.OpenAsync();

            //  ws.Send("hello");
            Colorful.Console.WriteLine("message was sent");
        }

        private void Ws_Opened(object sender, EventArgs e)
        {
            IsConnected = true;
            Colorful.Console.WriteLine("Opened");
        }


        private void Ws_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            Colorful.Console.WriteLine($"error: {e.Exception.ToString()}", Color.Red);


        }

        private void Ws_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Colorful.Console.WriteLine("data received", Color.Tomato);

        }

        private void Ws_Closed(object sender, EventArgs e)
        {
            IsConnected = false;
            Colorful.Console.WriteLine("closed", Color.Red);


            foreach (var taskCompletionSource in _waitForResp)
            {
                taskCompletionSource.Value.SetException(new Exception());

            }
            _waitForResp.Clear();

            TryToReconnect();
        }

        private void TryToReconnect()
        {
            SetTimer();
        }


        Dictionary<Guid, TaskCompletionSource<object>> _waitForResp = new Dictionary<Guid, TaskCompletionSource<object>>();

        private void Ws_MessageReceived(object sender, MessageReceivedEventArgs e)
        {

            var response = JsonConvert.DeserializeObject<Request>(e.Message);

            if (_waitForResp.TryGetValue(response.ID, out var tcs))
            {
                tcs.SetResult(null);
                return;
            }




            if (response.Command == ValidCommand.ServerTime)
            {
                DoOnServerTime(e.Message);
            }
            else
            {
                DoShowMessage(e.Message);
            }
        }

        public event EventHandler<string> OnShowMessage;

        private void DoShowMessage(string e)
        {
            OnShowMessage?.Invoke(this, e);
        }

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            aTimer.Stop();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            new WebSocket("ws://127.0.0.1:8181");
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                e.SignalTime);
        }

        public event EventHandler<string> OnServerTime;

        protected virtual void DoOnServerTime(string e)
        {
            OnServerTime?.Invoke(this, e);
        }

        public void SendServerTimeRequest()
        {
            var request = new Request()
            {
                Command = ValidCommand.ServerTime,
                ID = Guid.NewGuid()
            };

            SendMessage(request);
        }


        public async Task<FileSystemEventArgs> WaitForFolderChange(string path)
        {
            var request = new Request()
            {
                Command = ValidCommand.WaitForFolderChange,
                Message = path,
                ID = Guid.NewGuid()
            };

            var tcs = new TaskCompletionSource<object>();
            _waitForResp.Add(request.ID, tcs);
            SendMessage(request);

            //disconnected

            await tcs.Task;

            return null;
        }




        public void DivNumbers(int a, int b)
        {
            var message = $"div {a} {b}";
            var request = new Request()
            {
                Command = ValidCommand.Division,
                Message = message
            };

            SendMessage(request);
        }

        private Guid GetUniqeId()
        {
            return Guid.NewGuid();
        }

        private void SendMessage(Request request)
        {
            request.ID = GetUniqeId();
            var json = JsonConvert.SerializeObject(request);
            webSocket.Send(json);
        }
    }
}
