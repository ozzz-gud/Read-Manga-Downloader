using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ClassLibrary.Base
{
    class MainPage : WebPage
    {
        private readonly bool isAfter18;
        private string UrlModifer => isAfter18 ? "?mtr=1" : "";
        private string mangaNameRu;
        private string mangaNameEn;
        private string mangaNameOrig;
        public string MangaNameRu
        {
            get => mangaNameRu;
            private set => mangaNameRu = value.GetRightFolderName();
        }
        public string MangaNameEn
        {
            get => mangaNameEn;
            private set => mangaNameEn = value.GetRightFolderName();
        }
        public string MangaNameOrig
        {
            get => mangaNameOrig;
            private set => mangaNameOrig = value.GetRightFolderName();
        }

        public MainPage(string url, HttpClient client) : base(url)
        {
            var htmlCode = client.GetStringAsync(url).Result;
            Parse(htmlCode);
            MangaNameRu = Html.QuerySelector(Resource.Selector_TitleNameRu).TextContent;
            MangaNameEn = Html.QuerySelector(Resource.Selector_TitleNameEn).TextContent;
            MangaNameOrig = Html.QuerySelector(Resource.Selector_TitleNameOrig).TextContent;
            isAfter18 = Html.QuerySelector(Resource.Selector_IsAfter18) != null;
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
                url = string.Concat(Resource.Host, url, UrlModifer );
                chapters.Add(new Chapter(url, name, ++index));
            }
            return chapters;
        }
    }
}
