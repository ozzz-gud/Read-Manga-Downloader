using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace ClassLibrary.Base
{
    public abstract class WebPage
    {
        private readonly HtmlParser Parser = new HtmlParser();
        protected IHtmlDocument Html { get; private set; }
        public string Url { get; }
        protected WebPage(string url)
        {
            Url = url;
        }
        protected void Parse(string htmlCode)
        {
            Html = Parser.ParseDocument(htmlCode);
        }
    }
}
