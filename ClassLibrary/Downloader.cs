using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class Downloader
    {
        private const int THREADS = 3;
        private Task[] tasks = new Task[THREADS];
        private WebClient[] webClients = new WebClient[THREADS];
        private object locker = new object();
        private static readonly string PathToMangaFolder;

        public event Action DownloadProgressChanged;
        public event Action TitleDownloaded;

        static Downloader()
        {
            var documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            PathToMangaFolder = Path.Combine(documentsFolder, "Manga");
        }
        public Downloader()
        {
            for (int i = 0; i < THREADS; i++)
            {
                webClients[i] = new WebClient() { Encoding = Encoding.UTF8 };
            }
        }

        public void DownloadPageAsync(Page page)
        {
            lock(locker)
            {
                int ind = WaitAnyTasks(tasks);
                tasks[ind] = new Task(() =>
                {
                    string str = webClients[ind].DownloadString(page.Url);
                    page.Pharse(str);
                });
                tasks[ind].Start();
            }
        }
        private void DownloadPage(Page page, int ind)
        {
            string str = webClients[ind].DownloadString(page.Url);
            page.Pharse(str);
        }
        private void DownloadChapter(Chapter chapter, string pathToTitleFolder, int ind)
        {
            var pathToChapterFolder = Path.Combine(pathToTitleFolder, chapter.Name);
            CheckDirectory(pathToChapterFolder);
            foreach (var image in chapter.Images)
            {
                var pathToImage = Path.Combine(pathToChapterFolder, image.Key);
                if (!File.Exists(pathToImage))
                {
                    webClients[ind].DownloadFile(image.Value, pathToImage);
                }
                chapter.Title.ImageDownloaded++;
                DownloadProgressChanged?.Invoke();
            }
        }
        public void DownloadTitle(Title title, string template=null)
        {
            Task.Run(() =>
            {
                var pathToTitleFolder = Path.Combine(PathToMangaFolder, title.Name);

                if (title.ChapterNumberForDownload == null)
                    title.ChapterNumberForDownload = GetChaptersNumberFromTemplate(template);
                foreach (var chapterNumber in title.ChapterNumberForDownload)
                {
                    lock(locker)
                    {
                        int ind = WaitAnyTasks(tasks);
                        tasks[ind] = new Task(() =>
                        {
                            DownloadPage(title.Chapters[chapterNumber], ind);
                            DownloadChapter(title.Chapters[chapterNumber], pathToTitleFolder, ind);
                            title.ChapterDownloaded += 1;
                            DownloadProgressChanged?.Invoke();
                        });
                        tasks[ind].Start();
                    }
                }
                WaitAllTasks(tasks);
                TitleDownloaded?.Invoke();
            });
        }

        private static void WaitAllTasks(params Task[] tasks)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] != null) tasks[i].Wait(-1);
            }
        }
        private static int WaitAnyTasks(params Task[] tasks)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i] == null) return i;
            }
            return Task.WaitAny(tasks,-1);
        }
        private static List<int> GetChaptersNumberFromTemplate(string template)
        {
            List<int> chaptersNumber = new List<int>();
            string[] chapters = template.Split(' ');

            foreach (var temp in chapters)
            {
                if (temp.Contains("-"))
                {
                    string[] indexes = temp.Split('-');
                    int ChapterStart = int.Parse(indexes[0]);
                    int ChapterEnd = int.Parse(indexes[1]);

                    for (int ind = ChapterStart - 1; ind < ChapterEnd; ind++)
                        chaptersNumber.Add(ind);
                }
                else
                    chaptersNumber.Add(int.Parse(temp) - 1);
            }
            return chaptersNumber;
        }
        private static void CheckDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
