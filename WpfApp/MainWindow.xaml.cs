using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassLibrary.Base;
using ClassLibrary.Downloader;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddWindow.TitleAdded += AddWindow_TitleAdded;
            TitleDownloader.DownloadProgresChanged += Title_DownloadProgressChanged;
            TitleDownloader.Downloaded += Title_Downloaded;
        }
        private readonly TitleDownloader TitleDownloader = new TitleDownloader(3);
        private readonly List<TaskDownloadTitle> downloadTasks = new List<TaskDownloadTitle>();

        private void Title_Downloaded(TaskDownloadTitle task)
        {
            Dispatcher.Invoke(() =>
            {
                int row = task.IndexNumber;
                ListView.Items[row] = new { task.Title.NameRu, ProgrssInPersent = "Complete" };
            });
        }
        private void Title_DownloadProgressChanged(TaskDownloadTitle task)
        {
            Dispatcher.Invoke(() =>
            {
                int row = task.IndexNumber;
                ListView.Items[row] = new { task.Title.NameRu, ProgrssInPersent=task.Progress.ProgrssInPersent };
            });
        }

        private void AddWindow_TitleAdded(TaskDownloadTitle task)
        {
            Focus();
            downloadTasks.Add(task);
            TitleDownloader.AddTaskToDownload(task);
            ListView.Items.Add(new { task.Title.NameRu, task.Progress.ProgrssInPersent });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
        }
    }
}
