using System;
using System.Reflection;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int progressTop = 0;
            Title.ChapterListUpdated += (Title t) =>
            {
                progressTop = Console.CursorTop;
                t.Download(args[1]);
            };
            Title.Downloaded += (Title t) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.CursorTop = progressTop;
                Console.Write($"{t.Name} downloaded at {t.DownloadProgress}");
                Console.ResetColor();
                Environment.Exit(0);
            };
            Title.DownloadProgressChanged += (Title t) =>
            {
                Console.CursorTop = progressTop;
                Console.Write($"{t.Name} downloaded at {t.DownloadProgress}");
            };

            if(args!=null && args.Length == 2)
            {
                Title title = new Title(args[0]);
                while (Console.ReadKey(true).Key != ConsoleKey.Q) { }
            }
            else
            {
                var text = $"Скачивания манги с сайта ReadManga.me{Environment.NewLine}" +
                    $"Для скачивания манги со страницы [http://url] глав [1-14] напишите:{Environment.NewLine}" +
                    $"{Assembly.GetCallingAssembly().GetName().Name} [http://url] [1-14]";
                Console.WriteLine(text);
            }
        }
    }
}
