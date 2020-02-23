using AngleSharp.Html.Dom;

namespace ClassLibrary
{
    public abstract class WebPage
    {
        public IHtmlDocument Html;
        public readonly string Url;

        protected WebPage(string url)
        {
            Url = url;
        }
    }
}
