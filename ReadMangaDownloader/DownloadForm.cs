using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormApp
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
            Title.DownloadProgressChanged += Title_DownloadProgressChanged;
            Title.Downloaded += Title_Downloaded;
        }
        List<Title> titles = new List<Title>();

        private void Title_Downloaded(Title sender)
        {
            Invoke(new Action(() =>
            {
                int row = titles.FindIndex((t) => t.Id == sender.Id);
                dataGridView.Rows[row].Cells[1].Value = "Complete";
            }));
        }
        private void Title_DownloadProgressChanged(Title sender)
        {
            Invoke(new Action(() =>
            {
                int row = titles.FindIndex((t) => t.Id == sender.Id);
                dataGridView.Rows[row].Cells[1].Value = sender.DownloadProgress;
            }));
        }
        private void AddForm_TitleAdded(Title title)
        {
            Focus();
            titles.Add(title);

            dataGridView.Rows.Add();
            dataGridView.Rows[titles.Count - 1].Cells[0].Value = title.Name;
            dataGridView.Rows[titles.Count - 1].Cells[1].Value = "";
            title.Download();
        }
        private void AddManga_button_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.Show();
        }
    }
}
