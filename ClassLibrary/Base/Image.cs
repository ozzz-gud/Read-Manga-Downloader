using System.IO;

namespace ClassLibrary.Base
{
    public class Image
    {
        public string Url { get; }
        public string FullName { get; }
        public string FolderToDownload { get; set; }
        public string FullPath => Path.Combine(FolderToDownload, FullName);

        public Image(string fullName, string url)
        {
            FullName = fullName;
            Url = url;
        }
    }
}
