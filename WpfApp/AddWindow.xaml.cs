using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ClassLibrary.Base;
using ClassLibrary.Downloader;

namespace WpfApp
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private readonly Binding binding = new Binding() { ElementName = "CheckAll", Path = new PropertyPath("IsChecked"), Mode = BindingMode.OneWay };
        private Title title;
        public static event Action<TaskDownloadTitle> TitleAdded;
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            string url = Url.Text;
            Task.Run(() =>
            {
                title = new Title(url);
            }).ContinueWith(t =>
            {
                WriteChapterList(title);
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void WriteChapterList(Title title)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                ChapterListBox.Items.Clear();
                foreach (var chapter in title.Chapters)
                {
                    var checkBox = new CheckBox() { Content = chapter.Name };
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
                    ChapterListBox.Items.Add(checkBox);
                }
            }));

        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (title != null)
            {
                List<int> ChapterNumberForDownload = new List<int>();
                for (int i = 0; i < ChapterListBox.Items.Count; i++)
                {
                    var checkBox = ((CheckBox)ChapterListBox.Items[i]);
                    if (checkBox.IsChecked.Value)
                        ChapterNumberForDownload.Add(i+1);
                }
                TaskDownloadTitle downloadTask =
                    new TaskDownloadTitle(title, ChapterNumberForDownload, @"C:\Users\Vasiliy\Documents\Manga");

                Close();
                TitleAdded?.Invoke(downloadTask);
            }
        }
    }
}
