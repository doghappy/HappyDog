using HappyDog.WindowsUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Controls
{
    public sealed partial class ArticleList : UserControl, INotifyPropertyChanged
    {
        public ArticleList()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Article> articles;
        public ObservableCollection<Article> Articles
        {
            get => articles;
            set
            {
                articles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Articles)));
            }
        }

        private void AdaptiveGridViewControl_DataContextChanged(Windows.UI.Xaml.FrameworkElement sender, Windows.UI.Xaml.DataContextChangedEventArgs args)
        {
            var aa = args;
        }
    }
}
