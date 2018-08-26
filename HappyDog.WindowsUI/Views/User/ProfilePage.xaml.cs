using HappyDog.WindowsUI.ViewModels.User;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.User
{
    public sealed partial class ProfilePage : Page, INotifyPropertyChanged
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ProfileViewModel viewModel;
        public ProfileViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ViewModel)));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel = new ProfileViewModel();
        }


        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SignOut();
        }
    }
}
