﻿using HappyDog.WindowsUI.Services;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class DotNetPage : Page, INotifyPropertyChanged
    {
        public DotNetPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DotNetViewModel viewModel;
        public DotNetViewModel ViewModel
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
            NavigationCacheMode = Configuration.ArticlePageCache;
            ViewModel = new DotNetViewModel();
            await viewModel.InitializeAsync();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            if (Configuration.ArticlePageCache == NavigationCacheMode.Disabled)
            {
                NavigationCacheMode = NavigationCacheMode.Enabled;
                Configuration.ArticlePageCache = NavigationCacheMode;
            }
        }
    }
}
