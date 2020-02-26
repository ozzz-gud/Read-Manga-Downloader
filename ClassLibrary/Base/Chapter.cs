using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClassLibrary.Base
{
    public class Chapter : WebPage, IEnumerable<Image>
    {
        public int IndexNumber { get; }
        public string Name { get; }
        public List<Image> Images { get; } = new List<Image>();

        public Chapter(string url, string name, int index) : base(url)
        {
            Name = name.GetRightFolderName();
            IndexNumber = index;
        }
        public async Task UpdateChapterInfoAsync(HttpClient client)
        {
            var htmlCode = await client.GetStringAsync(Url);
            Parse(htmlCode);
            UpdateImageList();
        }
        private void UpdateImageList()
        {
            string[] links = GetLinks();
            for (int i = 0; i < links.Length; i++)
            {
                string imageName = i.ToString("d3");
                string extension = links[i].Substring(links[i].LastIndexOf("."), 4);
                string fullName = $"{imageName}{extension}";
                var image = new Image(fullName, links[i]);
                Images.Add(image);
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
        public override string ToString()
        {
            return $"{IndexNumber}) {Name}";
        }

        public IEnumerator<Image> GetEnumerator()
        {
            return ((IEnumerable<Image>)Images).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Image>)Images).GetEnumerator();
        }
    }
}
