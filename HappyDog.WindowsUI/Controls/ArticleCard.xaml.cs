using HappyDog.WindowsUI.Models;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Controls
{
    public sealed partial class ArticleCard : UserControl, INotifyPropertyChanged
    {
        public ArticleCard()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Article article;
        public Article Article
        {
            get => article;
            set
            {
                article = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Article)));
            }
        }
    }
}
