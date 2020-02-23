using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Title: IEnumerable<Chapter>
    {
        public int Id { get; private set; }
        public Chapter this[int index]
        {
            get => Chapters[index];
        }
        public string NameRu
        {
            get => mainPage.MangaNameRu;
        }
        public string NameEn
        {
            get => mainPage.MangaNameEn;
        }
        public string NameOrig
        {
            get => mainPage.MangaNameOrig;
        }

        private List<Chapter> Chapters;
        private MainPage mainPage;
        private static int numberTitles = 0;

        public Title(string url, bool RunAsync)
        {
            Id = numberTitles++;
            if (RunAsync)
                InitAsync(url);
            else
                Init(url);
        }

        private void Init(string url)
        {
            mainPage = new MainPage(url);
            Chapters = mainPage.GetContents();
        }
        private async void InitAsync(string url)
        {
            await Task.Run(() =>
            {
                Init(url);
            });
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
