using System;
using System.Reflection;
using ClassLibrary.Downloader;
using ClassLibrary.Base;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        private static int progressTop = 0;
        private static string pathToDownload;
        private static string progName;
        static void Main(string[] args)
        {
            progName = Assembly.GetCallingAssembly().GetName().Name;
            pathToDownload = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Manga");

            TitleDownloader.DownloadProgresChanged += TitleDownloader_DownloadProgresChanged;
            TitleDownloader.Downloaded += TitleDownloader_Downloaded;

            if (args.Length == 0)
            {
                Console.WriteLine(Resource.MainInfo, progName);
                Environment.Exit(0);
            }
            TitleDownloader downloader = new TitleDownloader(1);
            if (args.Length == 2)
            {
                Title title = new Title(args[0]);
                progressTop = Console.CursorTop;
                downloader.AddTitleToDownload(title, args[1], pathToDownload);
            }
            else if (args.Length == 1)
            {
                Title title = new Title(args[0]);
                WriteContents(title);
                var template = Console.ReadLine();
                downloader.AddTitleToDownload(title, template, pathToDownload);
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Q) { }
        }
        private static void WriteContents(Title title)
        {
            foreach (Chapter chapter in title)
            {
                Console.WriteLine(chapter);
            }
            Console.WriteLine(Resource.AdditionalInfo, progName);
            progressTop = Console.CursorTop;
        }

        private static void TitleDownloader_Downloaded(TaskDownloadTitle task)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorTop = progressTop;
            Console.CursorLeft = 0;
            Console.Write($"{task.Title.NameRu} downloaded at 100%");
            Console.ResetColor();
            Environment.Exit(0);
        }

        private static object locker = new object();
        private static void TitleDownloader_DownloadProgresChanged(TaskDownloadTitle task)
        {
            Console.CursorTop = progressTop;
            Console.CursorLeft = 0;
            Console.Write($"{task.Title.NameRu} downloaded at {task.Progress.ProgrssInPersent}%");
        }
    }
}
