using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace ReadMangaDownloader
{
    class Downloader
    {
        IHtmlDocument mainPage;
        private string TitleName;
        bool after18 = false;
        private HtmlParser parser = new HtmlParser();
        private WebClient client = new WebClient();

        private ProgressForm progressForm;

        public Dictionary<string, string> chapters = new Dictionary<string, string>();

        public Downloader(string url, ProgressForm progress)
        {
            progressForm = progress;
            client.Encoding = Encoding.UTF8;
            client.QueryString.Add("Host", "readmanga.me");
            client.QueryString.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36 OPR/56.0.3051.52");
            mainPage = DownloadPage(url);
            UpdateChaptersList(mainPage);
            TitleName = mainPage.QuerySelector("div.leftContent span.name").TextContent;
            after18 = mainPage.QuerySelector("div.mtr-message") != null;
        }
        private IHtmlDocument DownloadPage(string url)
        {
            string html = client.DownloadString(url);
            return parser.Parse(html);
        }
        private void UpdateChaptersList(IHtmlDocument mainPage)
        {
            chapters.Clear();
            var links = mainPage.QuerySelectorAll("div.chapters-link tbody a").Reverse();
            foreach (var a in links)
            {
                string name = a.TextContent;
                name = name.Replace("\n", "");
                name = Regex.Replace(name, @"\s{2,}", " ");
                if (chapters.ContainsKey(name))
                    name = name + "(2)";
                string url = a.GetAttribute("href");
                chapters.Add(name, $"http://readmanga.me/{url}");
            }
        }

        private Dictionary<string, string> GetImagesFromChapter(string chapterUrl)
        {
            IHtmlDocument page = DownloadPage($"{chapterUrl}{(after18 ? "?mtr=1":"")}");
            string code = page.QuerySelector("script:nth-child(4)").TextContent;
            List<string> links = GetLinks(code);
            Dictionary<string, string> Images = new Dictionary<string, string>();
            for (int i = 0; i < links.Count; i++)
            {
                string name = i.ToString("d3") + links[i].Substring(links[i].LastIndexOf("."),4); ;
                Images.Add(links[i], name);
            }
            return Images;
        }

        private void DownloadChapter(string chapterName, string url)
        {
            int doneImages = 0;
            string correctName = Regex.Replace(chapterName, @"[!?<=>:;,\/\\\|\.\[\]\*]", "").Replace("\"", "");
            string pathFolder = string.Concat($@"C:\Users\{Environment.UserName}\Documents\Manga\{TitleName}\{correctName}");
            if(!Directory.Exists(pathFolder))
                Directory.CreateDirectory(pathFolder);
            var images = GetImagesFromChapter(chapters[chapterName]);
            foreach (var _ in images)
            {
                progressForm.SetImageProgress(doneImages, images.Count, _.Value);
                Uri uri = new Uri(_.Key);
                client.DownloadFile(uri, $"{pathFolder}\\{_.Value}");
                doneImages += 1;
            }
            progressForm.SetImageProgress(doneImages, images.Count, "");
        }
        public void DownloadAll(CheckedListBox.CheckedItemCollection chaptersName)
        {
            int doneChapters = 0;
            foreach (string chapter in chaptersName)
            {
                progressForm.SetChapterProgress(doneChapters, chaptersName.Count, chapter);
                string chapterUrl = chapters[chapter];
                DownloadChapter(chapter, chapterUrl);
                doneChapters += 1;
            }
            progressForm.SetChapterProgress(doneChapters, chaptersName.Count, "");
        }

        private List<string> GetLinks(string text)
        {
            text = text.Remove(0, text.IndexOf("rm_h.init"));
            text = text.Remove(text.LastIndexOf("]"));
            text = text.Remove(0, text.IndexOf("[") + 1);
            text = text.Replace("'',", "");
            Regex regex = new Regex(@",\d+,\d+");
            text = regex.Replace(text, "");
            text = text.Replace("',\"", "");
            text = text.Replace("['", "");
            text = text.Replace("\"]", "");
            List<string> links = new List<string>();
            while (text.Length != 0)
            {
                string link = GetNextLink(ref text);
                links.Add(link);
            }
            return links;
        }
        private string GetNextLink(ref string text)
        {
            int length = text.IndexOf(",");
            string link;
            if (length == -1)
            {
                link = text.Substring(0);
                text = text.Remove(0);
            }
            else
            {
                link = text.Substring(0, length);
                text = text.Remove(0, length + 1);
            }

            return link;
        }

    }
}
