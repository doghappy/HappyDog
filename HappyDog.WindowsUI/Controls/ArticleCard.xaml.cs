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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var home = this.GetParent<HomePage>();
            if (home != null)
            {
                Frame frame = home.GetParent<Frame>("ContentFrame");
                if (frame != null)
                {
                    Type target = null;
                    switch (Article.CategoryId)
                    {
                        case 1: target = typeof(DotNetPage); break;
                        case 2: target = typeof(DatabasePage); break;
                        case 3: target = typeof(WindowsPage); break;
                        case 4: target = typeof(ReadPage); break;
                        case 5: target = typeof(EssaysPage); break;
                    }
                    if (target != null)
                    {
                        frame.Navigate(target);
                    }
                }
            }
        }
    }
}
