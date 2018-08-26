using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using HappyDog.WindowsUI.Views.Article;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
            var firstItem = NavView.MenuItems[0] as NavigationViewItem;
            firstItem.IsSelected = true;

            NavView.SelectionChanged += NavView_SelectionChanged;
            //SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        //private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        //{
        //    if (ContentFrame.CanGoBack && !e.Handled)
        //    {
        //        e.Handled = true;
        //        ContentFrame.GoBack();
        //    }
        //}

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrame.CanGoBack
            //    ? AppViewBackButtonVisibility.Visible
            //    : AppViewBackButtonVisibility.Collapsed;
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
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
                    case "user":
                        ContentFrame.Navigate(typeof(SignInPage));
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
            if (args.QueryText == "cmd>login();")
            {
                ContentFrame.Navigate(typeof(SignInPage));
            }
        }
    }
}
