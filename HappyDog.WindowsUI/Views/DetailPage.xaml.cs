using HappyDog.WindowsUI.Models;
using HappyDog.WindowsUI.ViewModels;
using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.ComponentModel;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
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
            if (e.NavigationMode == NavigationMode.New)
            {
                if (e.Parameter is int articleId)
                {
                    ViewModel = new DetailViewModel(articleId);
                    await ViewModel.InitializeAsync();
                }
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
    }
}
