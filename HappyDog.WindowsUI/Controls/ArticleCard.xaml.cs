using HappyDog.WindowsUI.Models;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Views.Article;
using System;

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
