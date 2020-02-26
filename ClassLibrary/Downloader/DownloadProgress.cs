namespace ClassLibrary.Downloader
{
    public class DownloadProgress
    {
        public int CountImages { get; set; }
        public int DownloadedImages = 0;
        public int ProgrssInPersent
        {
            get => (DownloadedImages * 100) / CountImages;
        }
    }
}
