using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Chapter : Page
    {
        public Dictionary<string, string> Images { get; private set; }
        public Chapter(string url, string name, Title titel) : base(url, titel)
        {
            Name = name;
            Pharsed += UpdateImageList;
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
