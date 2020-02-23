using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Chapter : WebPage
    {
        private static HtmlParser parser = new HtmlParser();
        private static WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
        private static object locker = new object();

        public int Index { get; private set; }
        public string Name { get; private set; }
        public int CountImage { get => Images.Count; }
        public Dictionary<string, string> Images { get; private set; }

        public Chapter(string url, string name, int index) : base(url)
        {
            Name = name.GetRightName();
            Index = index;
        }
        public async void UpdateChapterInfoAsync()
        {
            await Task.Run(() =>
            {
                UpdateChapterInfo();
            });
        }
        public void UpdateChapterInfo()
        {
            lock (locker)
            {
                var code = webClient.DownloadString(Url);
                Html = parser.ParseDocument(code);
                UpdateImageList();
            }
        }
        private void UpdateImageList()
        {
            Images = new Dictionary<string, string>();
            string[] links = GetLinks();
            for (int i = 0; i < links.Length; i++)
            {
                string imageName = i.ToString("d3");
                string extension = links[i].Substring(links[i].LastIndexOf("."), 4);
                string fullName = $"{imageName}{extension}";
                Images.Add(fullName, links[i]);
            }
        }
        private string[] GetLinks()
        {
            string text = Html.QuerySelector(Resource.Selector_scriptWithListImages).TextContent;
            text = text.Remove(0, text.IndexOf("[[") + 1);
            text = text.Remove(text.LastIndexOf("]"));

            text = Regex.Replace(text, "\\['','", "");
            text = Regex.Replace(text, "\",\\d+,\\d+\\]", "");
            text = Regex.Replace(text, "',\"", "");

            return text.Split(','); ;
        }
    }
}
