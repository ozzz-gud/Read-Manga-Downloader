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
        private readonly Regex regex = new Regex("('.*?'),(.*?),(\".*?\")", RegexOptions.Compiled);
        private string[] GetLinks()
        {
            List<string> Urls = new List<string>();
            string text = Html.QuerySelector(Resource.Selector_scriptWithListImages).TextContent;

            var matches = regex.Matches(text);
            foreach (Match match in matches)
            {
                var host = match.Groups[1].Value.Trim('\'');
                var addres = match.Groups[3].Value.Trim('"');
                Urls.Add(string.Concat(host,addres));
            }

            return Urls.ToArray();
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
