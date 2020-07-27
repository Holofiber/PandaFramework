using FolderWatcher;
using Library;
using System;

namespace FolderWatcherClient.Requests
{
    public class SubscribeRequest : IBaseMessage
    {
        public string TypeName { get; } = nameof(SubscribeRequest);
        public string Path { get; set; }
        public Guid ID { get; set; }
    }
}
