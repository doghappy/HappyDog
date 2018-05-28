using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HappyDog.WindowsUI.Common;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.Security.Authentication.Web;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class LoginPage : Page, INotifyPropertyChanged
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private LoginViewModel viewModel;
        public LoginViewModel ViewModel
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
            ViewModel = new LoginViewModel();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.LoginAsync();
            if (result.Code == CodeResult.OK)
            {
                Frame.Navigate(typeof(HomePage));
            }
            else
            {
                var dialog = new ContentDialog
                {
                    Title = "提示",
                    Content = result.Message,
                    PrimaryButtonText = "确定"
                };
                await dialog.ShowAsync();
            }
            //AccountsSettingsPane.Show();
        }

        private void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
