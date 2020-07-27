using Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace FolderWatcherServer
{
    public class FolderChangesResponse : IBaseMessage
    {
        public string TypeName { get; } = nameof(FolderChangesResponse);
        public string Message { get; }
    }
}
