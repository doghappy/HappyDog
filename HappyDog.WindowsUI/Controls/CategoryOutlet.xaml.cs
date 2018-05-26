using HappyDog.WindowsUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Controls
{
    public sealed partial class CategoryOutlet : UserControl, INotifyPropertyChanged
    {
        public CategoryOutlet()
        {
            InitializeComponent();
        }

       public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty JumbotronProperty =
            DependencyProperty.Register("Jumbotron", typeof(UIElement), typeof(CategoryOutlet), new PropertyMetadata(null));

        public UIElement Jumbotron
        {
            get => GetValue(JumbotronProperty) as UIElement;
            set => SetValue(JumbotronProperty, value);
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsLoading)));
            }
        }

        public ObservableCollection<Article> Articles { get; set; }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }
    }
}
