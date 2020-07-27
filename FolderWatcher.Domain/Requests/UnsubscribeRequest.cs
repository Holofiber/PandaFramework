using Library;

namespace FolderWatcherClient.Requests
{
    public class UnsubscribeRequest : IBaseMessage
    {
        public string TypeName { get; } = nameof(UnsubscribeRequest);
        public string Message { get; set; }
    }
}
