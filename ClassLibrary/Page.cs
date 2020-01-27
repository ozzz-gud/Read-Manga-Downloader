using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public abstract class Page
    {
        public event Action Pharsed;

        private string name;
        private static readonly HtmlParser parser = new HtmlParser();
        public string Name
        {
            get => name;
            set => name = GetRightName(value);
        }
        public readonly string Url;
        public readonly Title Title;
        public IHtmlDocument Html { get; private set; }
        public Page(string url, Title title)
        {
            Url = url;
            Title = title;
        }
        public void Pharse(string text)
        {
            Html = parser.ParseDocument(text);
            Pharsed?.Invoke();
        }
        private static string GetRightName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, ' ');
            }
            foreach (var c in Path.GetInvalidPathChars())
            {
                name = name.Replace(c, ' ');
            }
            name = Regex.Replace(name, @"\s{2,}", " ");

            return name;
        }
    }
}
