namespace ClassLibrary.Downloader
{
    public class DownloadProgress
    {
        public int CountImages { get; set; }
        public int DownloadedImages = 0;
        public int ProgrssInPersent
        {
            get
            {
                try
                {
                    return (DownloadedImages * 100) / CountImages;
                }
                catch (System.Exception)
                {
                    return 0;
                }
            }
        }
    }
}
