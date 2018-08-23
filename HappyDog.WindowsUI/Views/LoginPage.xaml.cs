using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.ViewModels;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
            //var result = await ViewModel.LoginAsync();
            //if (result.Code == CodeResult.OK)
            //{
            //    Frame.Navigate(typeof(HomePage));
            //}
            //else
            //{
            //    var dialog = new ContentDialog
            //    {
            //        Title = "提示",
            //        Content = result.Message,
            //        PrimaryButtonText = "确定"
            //    };
            //    await dialog.ShowAsync();
            //}
        }
    }
}
