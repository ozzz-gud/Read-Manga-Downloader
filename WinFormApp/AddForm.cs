using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary.Base;
using ClassLibrary.Downloader;

namespace WinFormApp
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }
        private void AddForm_Load(object sender, EventArgs e)
        {
            PathToDownload_textBox.Text =
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Manga"
                    );
        }
        public static event Action<TaskDownloadTitle> TitleAdded;
        private Title title;
        private void Show_button_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                title = new Title(url_textBox.Text);
            }).ContinueWith(t =>
            {
                WriteChapterList(title);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void WriteChapterList(Title title)
        {
            Invoke(new Action(() =>
            {
                chapterList_checkedListBox.Items.Clear();
                chapterList_checkedListBox.Items.AddRange(title.Chapters.ToArray());
            }));
        }

        private bool checkedAll = true;
        private void CheckAll_button_Click(object sender, EventArgs e)
        {
            int count = chapterList_checkedListBox.Items.Count;
            for (int i = 0; i < count; i++)
                chapterList_checkedListBox.SetItemChecked(i, checkedAll);
            checkedAll = !checkedAll;
        }
        private void Add_button_Click(object sender, EventArgs e)
        {
            if (title != null)
            {
                var checkedIndexes = chapterList_checkedListBox.CheckedIndices;
                List<int> ChapterNumberForDownload = new List<int>();

                foreach (int indexChecked in checkedIndexes)
                    ChapterNumberForDownload.Add(indexChecked);
                string pathToDownload = PathToDownload_textBox.Text;
                TaskDownloadTitle downloadTask = 
                    new TaskDownloadTitle(title, ChapterNumberForDownload, pathToDownload);
                Close();
                TitleAdded?.Invoke(downloadTask);
            }
        }
    }
}
