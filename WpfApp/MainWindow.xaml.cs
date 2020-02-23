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
using ClassLibrary;

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
            ClassLibrary.Title.DownloadProgressChanged += Title_DownloadProgressChanged;
            ClassLibrary.Title.Downloaded += Title_Downloaded;
        }

        List<Title> titles = new List<Title>();
        private void Title_Downloaded(Title title)
        {
            Dispatcher.Invoke(() =>
            {
                int row = titles.FindIndex((t) => t.Id == title.Id);
                ListView.Items[row] = new { title.NameRu, DownloadProgress = "Complete" };
            });
        }
        private void Title_DownloadProgressChanged(Title title)
        {
            Dispatcher.Invoke(() =>
            {
                ListView.Items.Refresh();
            });
        }

        private void AddWindow_TitleAdded(Title title)
        {
            Focus();
            titles.Add(title);
            ListView.Items.Add(title);
            title.Download();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addWindow = new AddWindow();
            addWindow.Show();
        }
    }
}
