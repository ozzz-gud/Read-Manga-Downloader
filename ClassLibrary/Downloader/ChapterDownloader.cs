using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ClassLibrary.Base;

namespace ClassLibrary.Downloader
{
    class ChapterDownloader
    {
        private readonly string pathToDownload;
        private readonly ConcurrentQueue<Image> queue = new ConcurrentQueue<Image>();
        private readonly HttpClient client;

        public event Action DownloadProgresChanged;
        public event Action Downloaded;

        public ChapterDownloader(string pathToDownload)
        {
            this.pathToDownload = pathToDownload;
            client = new HttpClient();
        }
        public async Task<int> AddToDownload(Chapter chapter)
        {
            var FolderWithImages = Path.Combine(pathToDownload, chapter.Name);
            await chapter.UpdateChapterInfoAsync(client);
            foreach (var image in chapter.Images)
            {
                image.FolderToDownload = FolderWithImages;
                queue.Enqueue(image);
            }
            return chapter.Images.Count;
        }
        public async void StartDownload()
        {
            List<Task> tasks = new List<Task>();
            while (queue.TryDequeue(out Image img))
            {
                CheckOrCreateDirectory(img.FolderToDownload);
                if (!File.Exists(img.FullPath))
                {
                    tasks.Add(DownloadImage(img));
                }
            }
            await Task.WhenAll(tasks);
            Downloaded?.Invoke();
        }
        private async Task DownloadImage(Image image)
        {
            var response = await client.GetAsync(image.Url);
            if(response.IsSuccessStatusCode)
            {
                var content = response.Content;
                var contentStream = await content.ReadAsStreamAsync();
                var stream = File.Create(image.FullPath);
                await contentStream.CopyToAsync(stream);
                DownloadProgresChanged?.Invoke();
            }
        }
        private void CheckOrCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
