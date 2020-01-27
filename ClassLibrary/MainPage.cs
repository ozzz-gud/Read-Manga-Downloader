using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary
{
    class MainPage : Page
    {
        private bool isAfter18;
        public List<Chapter> Chapters { get; private set; }
        public MainPage(string url, Title title) : base(url, title)
        {
            Pharsed += () =>
            {
                Name = Html.QuerySelector(Resource.Selector_TitleName).TextContent;
                isAfter18 = Html.QuerySelector(Resource.Selector_IsAfter18) != null;
                UpdateChaptersList();
            };
        }
        private void UpdateChaptersList()
        {
            Chapters = new List<Chapter>();
            var links = Html.QuerySelectorAll(Resource.Selector_ChapterLinks).Reverse();
            foreach (var item in links)
            {
                string name = item.TextContent;
                string url = item.GetAttribute("href");
                url = string.Concat(Resource.Host, url, (isAfter18 ? "?mtr=1" : "") );
                Chapters.Add(new Chapter(url, name, Title));
            }
        }
    }
}
