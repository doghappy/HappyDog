using HappyDog.WindowsUI.ViewModels.Article;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.ComponentModel;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class DetailPage : Page, INotifyPropertyChanged
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DetailViewModel viewModel;
        public DetailViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ViewModel)));
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is int articleId)
            {
                ViewModel = new DetailViewModel(articleId);
                await ViewModel.InitializeAsync();
            }
        }

        private async void MarkdownTextBlock_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                await Launcher.LaunchUriAsync(new Uri(e.Link));
            }
            catch { }
        }

        private void Edit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditArticlePage), ViewModel.Article);
        }
    }
}
