using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace ClassLibrary.Base
{
    public class Title: IEnumerable<Chapter>
    {
        private static int numberOfTitles = 0;
        private readonly MainPage mainPage;
        private readonly HttpClient client = new HttpClient();

        public readonly int IndexNumber;
        public string NameRu => mainPage.MangaNameRu;
        public string NameEn => mainPage.MangaNameEn;
        public string NameOrig => mainPage.MangaNameOrig;
        public Chapter this[int index] => Chapters[index];
        public List<Chapter> Chapters { get; }

        public Title(string url)
        {
            IndexNumber = numberOfTitles++;
            mainPage = new MainPage(url, client);
            Chapters = mainPage.GetContents();
        }

        public IEnumerator<Chapter> GetEnumerator()
        {
            return ((IEnumerable<Chapter>)Chapters).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Chapter>)Chapters).GetEnumerator();
        }
    }
}
