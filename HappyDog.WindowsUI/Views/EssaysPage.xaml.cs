using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.ViewModels;
using System.ComponentModel;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class EssaysPage : ArticleListBasePage, INotifyPropertyChanged
    {
        public EssaysPage()
        {
            InitializeComponent();
            Configuration.CachedPages.Add(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private EssaysViewModel viewModel;
        public EssaysViewModel ViewModel
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
            ViewModel = new EssaysViewModel();
            await viewModel.InitializeAsync();
        }
    }
}
