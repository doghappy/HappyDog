﻿using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.ViewModels.Article;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class DatabasePage : Page, INotifyPropertyChanged
    {
        public DatabasePage()
        {
            InitializeComponent();
            Configuration.AddPageCache(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DatabaseViewModel viewModel;
        public DatabaseViewModel ViewModel
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
            ViewModel = new DatabaseViewModel();
            await viewModel.InitializeAsync();
        }
    }
}