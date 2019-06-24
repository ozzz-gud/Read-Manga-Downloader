using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadMangaDownloader
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }
        Downloader downloader;
        ProgressForm progress = new ProgressForm();

        private void show_button_Click(object sender, EventArgs e)
        {
            chapters_checkedListBox.Items.Clear();
            string url = url_textBox.Text;
            Task<Downloader> createDownloader = Task.Run(() =>
            {
                return new Downloader(url,progress);
            });
            Task taskShow = createDownloader.ContinueWith((prevTask) =>
            {
                downloader = prevTask.Result;
                chapters_checkedListBox.Invoke(new Action(() =>
                {
                    foreach (var chapter in downloader.chapters)
                        chapters_checkedListBox.Items.Add(chapter.Key);
                }));
            });
        }
        private bool checkedAll = true;

        private void check_button_Click(object sender, EventArgs e)
        {
            int count = chapters_checkedListBox.Items.Count;
            for (int i=0; i<count; i++)
                chapters_checkedListBox.SetItemChecked(i, checkedAll);
            checkedAll = !checkedAll;
        }

        private void download_button_Click(object sender, EventArgs e)
        {
            var ChaptersName = chapters_checkedListBox.CheckedItems;
            progress.Show();
            Task load = Task.Run(() =>
            {
                downloader.DownloadAll(ChaptersName);
            });
            Task end = load.ContinueWith((_) =>
            {
                MessageBox.Show("Загруженно");
                progress.Exit();
            });
        }
    }
}
