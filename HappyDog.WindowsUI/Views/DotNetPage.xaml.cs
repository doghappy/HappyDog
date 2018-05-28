using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class DotNetPage : ArticleListBasePage, INotifyPropertyChanged
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
            ViewModel = new DotNetViewModel();
            await viewModel.InitializeAsync();
        }
    }
}
