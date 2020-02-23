using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileDownloader
{
    public class Downloader
    {
        private const int THREADS = 3;
        private Task[] tasks = new Task[THREADS];
        private WebClient[] webClients = new WebClient[THREADS];
        private object locker = new object();
        private static readonly string PathToMangaFolder;

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
        private static Regex regex = new Regex(@"(?:(\d+)(-)?(\d+)?)");
        public static List<int> ParseTemplate(string template)
        {
            var rezult = new List<int>();
            var matches = regex.Matches(template);
            foreach (Match item in matches)
            {
                if(item.Groups[2].Value == "-")
                {
                    ParseComplicatedTemplate(rezult, item);
                }
                else
                {
                    var ind = int.Parse(item.Groups[1].Value);
                    rezult.Add(ind);
                }
            }
            return rezult;
        }
        private static void ParseComplicatedTemplate(List<int> rezult, Match item)
        {
            var start = int.Parse(item.Groups[1].Value);
            var stop = int.Parse(item.Groups[3].Value);
            for (int i = start; i <= stop; i++)
            {
                rezult.Add(i);
            }
        }
        private static void CheckOrCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
