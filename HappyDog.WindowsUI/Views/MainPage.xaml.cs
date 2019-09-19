using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HappyDog.WindowsUI.Views.Article;
using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Requesters;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            _articleRequester = new ArticleRequester();
            Articles = new ObservableCollection<Models.Article>();
        }

        private ArticleRequester _articleRequester;

        public ObservableCollection<Models.Article> Articles { get; }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
            var firstItem = NavView.MenuItems[0] as NavigationViewItem;
            firstItem.IsSelected = true;

            NavView.SelectionChanged += NavView_SelectionChanged;
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (ContentFrame.CanGoBack)
            {
                NavView.IsBackEnabled = true;
                NavView.IsBackButtonVisible = NavigationViewBackButtonVisible.Visible;
            }
            else
            {
                NavView.IsBackEnabled = false;
                NavView.IsBackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            }
            if (e.NavigationMode == NavigationMode.Back)
            {
                NavigationViewItem item = null;
                switch (e.SourcePageType)
                {
                    case Type t when e.SourcePageType == typeof(HomePage):
                        item = NavView.MenuItems[0] as NavigationViewItem;
                        break;
                    case Type t when e.SourcePageType == typeof(DotNetPage):
                        item = NavView.MenuItems[1] as NavigationViewItem;
                        break;
                    case Type t when e.SourcePageType == typeof(DatabasePage):
                        item = NavView.MenuItems[2] as NavigationViewItem;
                        break;
                    case Type t when e.SourcePageType == typeof(WindowsPage):
                        item = NavView.MenuItems[3] as NavigationViewItem;
                        break;
                    case Type t when e.SourcePageType == typeof(ReadPage):
                        item = NavView.MenuItems[4] as NavigationViewItem;
                        break;
                    case Type t when e.SourcePageType == typeof(EssaysPage):
                        item = NavView.MenuItems[5] as NavigationViewItem;
                        break;
                }
                if (item != null)
                {
                    item.IsSelected = true;
                }
            }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                var item = args.SelectedItem as NavigationViewItem;
                switch (item.Tag)
                {
                    case "home":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;
                    case ".net":
                        ContentFrame.Navigate(typeof(DotNetPage));
                        break;
                    case "db":
                        ContentFrame.Navigate(typeof(DatabasePage));
                        break;
                    case "windows":
                        ContentFrame.Navigate(typeof(WindowsPage));
                        break;
                    case "read":
                        ContentFrame.Navigate(typeof(ReadPage));
                        break;
                    case "essays":
                        ContentFrame.Navigate(typeof(EssaysPage));
                        break;
                }
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }

        private void SearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion == null)
            {
                if (!string.IsNullOrWhiteSpace(args.QueryText))
                {
                    ContentFrame.Navigate(typeof(SearchPage), args.QueryText);
                }
            }
            else
            {
                var article = args.ChosenSuggestion as Models.Article;
                ContentFrame.Navigate(typeof(DetailPage), article.Id);
                NavView.SelectionChanged -= NavView_SelectionChanged;
                NavView.SelectedItem = null;
                NavView.SelectionChanged += NavView_SelectionChanged;
                //sender.Text = string.Empty;
            }
        }

        private async void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                Articles.Clear();
                if (!string.IsNullOrWhiteSpace(sender.Text))
                {
                    var data = await _articleRequester.SearchArticlesAsync(sender.Text);
                    foreach (var item in data.Data.Data)
                    {
                        Articles.Add(item);
                    }
                }
            }
        }
    }
}
