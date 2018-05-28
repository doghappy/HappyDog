using HappyDog.WindowsUI.Services;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class ReadPage : ArticleListBasePage, INotifyPropertyChanged
    {
        public ReadPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ReadViewModel viewModel;
        public ReadViewModel ViewModel
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
            ViewModel = new ReadViewModel();
            await viewModel.InitializeAsync();
        }
    }
}
