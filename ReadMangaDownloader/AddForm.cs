using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormApp
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }
        private void AddForm_Load(object sender, EventArgs e)
        {
            Title.ChapterListUpdated += WriteChapterList;
        }
        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Title.ChapterListUpdated -= WriteChapterList;
        }

        private Title title;
        public static event Action<Title> TitleAdded;

        private void Show_button_Click(object sender, EventArgs e)
        {
            show_button.Enabled = false;
            title = new Title(url_textBox.Text);
            show_button.Enabled = true;
        }
        private void WriteChapterList(Title title)
        {
            Invoke(new Action(() =>
            {
                chapterList_checkedListBox.Items.Clear();
                foreach (var chapter in title.Chapters)
                    chapterList_checkedListBox.Items.Add(chapter.Name);
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

                title.ChapterNumberForDownload = ChapterNumberForDownload;
                Close();
                TitleAdded?.Invoke(title);
            }
        }
    }
}
