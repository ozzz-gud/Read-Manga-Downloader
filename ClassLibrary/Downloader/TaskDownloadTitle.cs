using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClassLibrary.Base;

namespace ClassLibrary.Downloader
{
    public class TaskDownloadTitle
    {
        public readonly Title Title;
        public readonly List<int> IndexNumberForDownload;
        public readonly string PathToDownload;
        public readonly DownloadProgress progress;

        public TaskDownloadTitle(Title title, string template, string pathToDownload)
        {
            Title = title;
            IndexNumberForDownload = ParseTemplate(template);
            PathToDownload = pathToDownload;
            progress = new DownloadProgress();
        }
        public TaskDownloadTitle(Title title, List<int> chaptersFormDownload, string pathToDownload)
        {
            Title = title;
            IndexNumberForDownload = chaptersFormDownload;
            PathToDownload = pathToDownload;
            progress = new DownloadProgress();
        }

        private static readonly Regex regex = new Regex(@"(?:(\d+)(-)?(\d+)?)", RegexOptions.Compiled);
        private static List<int> ParseTemplate(string template)
        {
            var rezult = new List<int>();
            var matches = regex.Matches(template);
            foreach (Match item in matches)
            {
                if (item.Groups[2].Value == "-")
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
            var startIndex = int.Parse(item.Groups[1].Value);
            var stopIndex = int.Parse(item.Groups[3].Value);
            for (int i = startIndex; i <= stopIndex; i++)
            {
                rezult.Add(i);
            }
        }
    }
}
