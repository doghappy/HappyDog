﻿using HappyDog.WindowsUI.ViewModels.User;
using System.ComponentModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class SignInPage : Page, INotifyPropertyChanged
    {
        public SignInPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private SignInViewModel viewModel;
        public SignInViewModel ViewModel
        {
            get => viewModel;
            private set
            {
                viewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ViewModel)));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new SignInViewModel();
            ViewModel.Initialize();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SignInAsync(false);
        }

        private async void PasswordBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                await ViewModel.SignInAsync(false);
            }
        }
    }
}
