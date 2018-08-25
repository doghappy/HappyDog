using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class SignInPage : Page, INotifyPropertyChanged
    {
        public SignInPage()
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

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SignInAsync();
        }
    }
}
