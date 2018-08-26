using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.Views.Article;
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

        private void AdaptiveGridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame frame = this.GetParent<Frame>("ContentFrame");
            if (frame != null)
            {
                var article = e.ClickedItem as Article;
                frame.Navigate(typeof(DetailPage), article.Id);
            }
        }
    }
}
