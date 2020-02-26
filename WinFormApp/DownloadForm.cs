using System;
using System.Windows.Forms;
using ClassLibrary.Base;
using ClassLibrary.Downloader;

namespace WinFormApp
{
    public partial class DownloadForm : Form
    {
        public DownloadForm()
        {
            InitializeComponent();
        }
        private void DownloadForm_Load(object sender, EventArgs e)
        {
            AddForm.TitleAdded += AddForm_TitleAdded;
            TitleDownloader.DownloadProgresChanged += Title_DownloadProgressChanged;
            TitleDownloader.Downloaded += Title_Downloaded;
        }

        readonly TitleDownloader TitleDownloader = new TitleDownloader(3);

        private void Title_Downloaded(TaskDownloadTitle task)
        {
            Invoke(new Action(() =>
            {
                int row = task.Title.IndexNumber;
                dataGridView.Rows[row].Cells[1].Value = "Complete";
            }));
        }
        private void Title_DownloadProgressChanged(TaskDownloadTitle task)
        {
            Invoke(new Action(() =>
            {
                int row = task.Title.IndexNumber;
                dataGridView.Rows[row].Cells[1].Value = $"{task.progress.ProgrssInPersent}%";
            }));
        }
        private void AddForm_TitleAdded(TaskDownloadTitle task)
        {
            Focus();
            TitleDownloader.AddTaskToDownload(task);

            dataGridView.Rows.Add();
            dataGridView.Rows[task.Title.IndexNumber].Cells[0].Value = task.Title.NameRu;
            dataGridView.Rows[task.Title.IndexNumber].Cells[1].Value = "";
        }
        private void AddManga_button_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }
    }
}
