using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

        private readonly TitleDownloader TitleDownloader = new TitleDownloader(3);
        private readonly List<TaskDownloadTitle> downloadTasks = new List<TaskDownloadTitle>();

        private void Title_Downloaded(TaskDownloadTitle task)
        {
            Invoke(new Action(() =>
            {
                int row = task.IndexNumber;
                dataGridView.Rows[row].Cells[1].Value = "Complete";
            }));
        }
        private void Title_DownloadProgressChanged(TaskDownloadTitle task)
        {
            Invoke(new Action(() =>
            {
                int row = task.IndexNumber;
                dataGridView.Rows[row].Cells[1].Value = $"{task.Progress.ProgrssInPersent}%";
            }));
        }
        private void AddForm_TitleAdded(TaskDownloadTitle task)
        {
            Focus();
            downloadTasks.Add(task);
            TitleDownloader.AddTaskToDownload(task);

            dataGridView.Rows.Add();
            dataGridView.Rows[task.IndexNumber].Cells[0].Value = task.Title.NameRu;
            dataGridView.Rows[task.IndexNumber].Cells[1].Value = "";
        }
        private void Add_button_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }

        private void Stop_button_Click(object sender, EventArgs e)
        {
            //if (dataGridView.SelectedRows.Count > 0)
            //{
            //    var index = dataGridView.SelectedRows[0].Index;
            //    var task = downloadTasks.Find(t => t.IndexNumber == index);
            //    task.cancellationTokenSource.Cancel();
            //}
        }
    }
}
