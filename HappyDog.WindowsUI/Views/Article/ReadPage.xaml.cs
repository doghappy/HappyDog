using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views.Article
{
    public sealed partial class ReadPage : Page, INotifyPropertyChanged
    {
        public ReadPage()
        {
            InitializeComponent();
            Configuration.CachedPages.Add(this);
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
