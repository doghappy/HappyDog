using HappyDog.WindowsUI.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Controls
{
    public sealed partial class CategoryOutlet : UserControl
    {
        public CategoryOutlet()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty JumbotronProperty =
            DependencyProperty.Register("Jumbotron", typeof(UIElement), typeof(CategoryOutlet), new PropertyMetadata(null));

        public UIElement Jumbotron
        {
            get => GetValue(JumbotronProperty) as UIElement;
            set => SetValue(JumbotronProperty, value);
        }

        public ArticleViewModel ViewModel { get; set; }

        private async void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset <= 140)
            {
                if (!e.IsIntermediate && ViewModel.HasMoreArticles)
                {
                    await ViewModel.LoadArticleAsync();
                }
            }
        }
    }
}
