using System.IO;

namespace Library
{
    public struct FileSystemEvent
    {
        public WatcherChangeTypes ChangesType { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }

        public string GetDirectoryName()
        {
            return Path.GetDirectoryName(FullPath);
        }

        public override string ToString()
        {
            return $"ChangesType: {ChangesType}| FileName: {FileName}| FullPath: {FullPath}";
        }
    }
}
