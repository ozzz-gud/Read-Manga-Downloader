using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    class MainPage : WebPage
    {
        private static HtmlParser parser = new HtmlParser();
        private static WebClient webClient = new WebClient() { Encoding = Encoding.UTF8 };
        private static object locker = new object();

        private bool isAfter18;
        private string modifer
        {
            get => isAfter18 ? "?mtr=1" : "";
        }
        private string mangaNameRu;
        private string mangaNameEn;
        private string mangaNameOrig;
        public string MangaNameRu
        {
            get => mangaNameRu;
            set => mangaNameRu = value.GetRightName();
        }
        public string MangaNameEn
        {
            get => mangaNameEn;
            set => mangaNameEn = value.GetRightName();
        }
        public string MangaNameOrig
        {
            get => mangaNameOrig;
            set => mangaNameOrig = value.GetRightName();
        }

        public MainPage(string url) : base(url)
        {
            lock (locker)
            {
                var code = webClient.DownloadString(url);
                Html = parser.ParseDocument(code);
                MangaNameRu = Html.QuerySelector(Resource.Selector_TitleNameRu).TextContent;
                MangaNameEn = Html.QuerySelector(Resource.Selector_TitleNameEn).TextContent;
                MangaNameOrig = Html.QuerySelector(Resource.Selector_TitleNameOrig).TextContent;
                isAfter18 = Html.QuerySelector(Resource.Selector_IsAfter18) != null;
            }
        }
        public List<Chapter> GetContents()
        {
            var chapters = new List<Chapter>();
            var links = Html.QuerySelectorAll(Resource.Selector_ChapterLinks).Reverse();
            var index = 0;
            foreach (var item in links)
            {
                string name = item.TextContent;
                string url = item.GetAttribute("href");
                url = string.Concat(Resource.Host, url, modifer );
                chapters.Add(new Chapter(url, name, ++index));
            }
            return chapters;
        }
    }
}
