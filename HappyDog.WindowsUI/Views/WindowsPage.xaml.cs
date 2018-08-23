﻿using HappyDog.WindowsUI.ViewModels;
using System;
using System.ComponentModel;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class WindowsPage : ArticleListBasePage, INotifyPropertyChanged
    {
        public WindowsPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private WindowsViewModel viewModel;
        public WindowsViewModel ViewModel
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
            ViewModel = new WindowsViewModel();
            await viewModel.InitializeAsync();
        }

        private async void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Uri uri = new Uri("https://www.microsoft.com/zh-cn/windows/reasons-to-upgrade-to-a-new-windows-10-pc");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
