using Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace FolderWatcher.Domain
{
    public class FolderChangesResponse : IBaseMessage
    {
        public string TypeName { get; } = nameof(FolderChangesResponse);
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string ChangesType { get; set; }
    }
}
