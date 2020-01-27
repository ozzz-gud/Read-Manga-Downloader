using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClassLibrary;

namespace WpfApp
{
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClassLibrary.Title.ChapterListUpdated += Title_ChapterListUpdated;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ClassLibrary.Title.ChapterListUpdated -= Title_ChapterListUpdated;
        }

        private readonly Binding binding = new Binding() { ElementName = "CheckAll", Path = new PropertyPath("IsChecked"), Mode = BindingMode.OneWay };
        private Title title;

        public static event Action<Title> TitleAdded;
        private void Show_Click(object sender, RoutedEventArgs e)
        {
            Show.IsEnabled = false;
            title = new Title(Url.Text);
            Show.IsEnabled = true;
        }
        private void Title_ChapterListUpdated(Title title)
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
                title.ChapterNumberForDownload = new List<int>();
                for (int i = 0; i < ChapterListBox.Items.Count; i++)
                {
                    var checkBox = ((CheckBox)ChapterListBox.Items[i]);
                    if (checkBox.IsChecked.Value)
                        title.ChapterNumberForDownload.Add(i);
                }

                Close();
                TitleAdded?.Invoke(title);
            }
        }
    }
}
