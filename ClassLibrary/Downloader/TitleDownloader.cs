using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using ClassLibrary.Base;

namespace ClassLibrary.Downloader
{
    public class TitleDownloader
    {
        private readonly Semaphore semaphore;
        private readonly ConcurrentQueue<TaskDownloadTitle> queue = new ConcurrentQueue<TaskDownloadTitle>();
        private readonly ManualResetEvent queueAddedEvent = new ManualResetEvent(false);

        public static event Action<TaskDownloadTitle> DownloadProgresChanged;
        public static event Action<TaskDownloadTitle> Downloaded;
        
        public TitleDownloader(int maxActiveDownloads)
        {
            semaphore = new Semaphore(maxActiveDownloads, maxActiveDownloads);
            CreateWatcherTask();
        }
        private void CreateWatcherTask()
        {
            Task.Run(() =>
            {
                while(true)
                {
                    queueAddedEvent.WaitOne();
                    semaphore.WaitOne();
                    if(queue.TryDequeue(out TaskDownloadTitle task))
                    {
                        CreateDonloadTask(task);
                    }
                    else
                    {
                        queueAddedEvent.Reset();
                    }
                }
            });
        }
        private void CreateDonloadTask(TaskDownloadTitle task)
        {
            Task.Run(() =>
            {
                var FolderWithChapters = Path.Combine(task.PathToDownload, task.Title.NameRu);
                ChapterDownloader downloader = new ChapterDownloader(FolderWithChapters);
                downloader.Downloaded += () => Downloaded(task);
                downloader.DownloadProgresChanged += () =>
                {
                    Interlocked.Increment(ref task.progress.DownloadedImages);
                    DownloadProgresChanged(task);
                };
                var listTask = from chapter in task.Title 
                           where task.IndexNumberForDownload.Contains(chapter.IndexNumber) 
                           select downloader.AddToDownload(chapter);
                Task<int>[] tasks = listTask.ToArray();
                int[] count = Task.WhenAll(tasks).Result;
                task.progress.CountImages = count.Sum();
                downloader.StartDownload();
            });
        }
        public void AddTitleToDownload(Title title, string template, string pathToDownload)
        {
            var downloadTask = new TaskDownloadTitle(title, template, pathToDownload);
            AddTaskToDownload(downloadTask);
        }
        public void AddTaskToDownload(TaskDownloadTitle task)
        {
            queue.Enqueue(task);
            queueAddedEvent.Set();
        }
    }
}
