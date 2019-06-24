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
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }
        public void SetChapterProgress(int complete, int all, string currentChapter)
        {
            Invoke(new Action(() =>
            {
                chapters_progressBar.Value = ((complete * 100) / all);
                chapter_label.Text = currentChapter;
            }));
        }
        public void SetImageProgress(int complete, int all, string currentImage)
        {
            Invoke(new Action(() =>
            {
                Image_progressBar.Value = ((complete * 100) / all);
                image_label.Text = currentImage;
            }));
        }
        public void Exit()
        {
            Invoke(new Action(() =>
            {
                Close();
            }));
        }
    }
}
