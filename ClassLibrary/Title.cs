using System;
using System.Collections.Generic;

namespace ClassLibrary
{
    public class Title
    {
        public static event Action<Title> ChapterListUpdated;
        public static event Action<Title> Downloaded;
        public static event Action<Title> DownloadProgressChanged;

        private readonly Downloader downloader;
        private readonly MainPage mainPage;

        private static int numberTitles = 0;
        public readonly int Id;

        public int ChapterDownloaded;
        public int ImageDownloaded;
        public string DownloadProgress
        {
            get => $@"C:{ChapterDownloaded}/{ChapterNumberForDownload.Count} I:{ImageDownloaded}";
        }

        public string Name
        {
            get => mainPage.Name;
        }
        public List<Chapter> Chapters
        {
            get => mainPage.Chapters;
        }
        public List<int> ChapterNumberForDownload;

        public Title(string url)
        {
            Id = numberTitles++;
            downloader = new Downloader();
            downloader.TitleDownloaded += () => Downloaded.Invoke(this);
            downloader.DownloadProgressChanged += () => DownloadProgressChanged.Invoke(this);
            mainPage = new MainPage(url, this);
            mainPage.Pharsed += () => ChapterListUpdated?.Invoke(this);
            downloader.DownloadPageAsync(mainPage);
        }
        public void Download(string template = null)
        {
            downloader.DownloadTitle(this,template);
        }
    }
}
