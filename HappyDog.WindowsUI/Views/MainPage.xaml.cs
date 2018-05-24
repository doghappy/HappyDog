using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
        }

        private void MainPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (ContentFrame.CanGoBack && !e.Handled)
            {
                e.Handled = true;
                ContentFrame.GoBack();
            }
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrame.CanGoBack
            //    ? AppViewBackButtonVisibility.Visible
            //    : AppViewBackButtonVisibility.Collapsed;
            //if (e.NavigationMode == NavigationMode.Back)
            //{
            //    NavigationViewItem item = null;
            //    switch (e.SourcePageType)
            //    {
            //        case Type t when e.SourcePageType == typeof(HomePage):
            //        case Type u when e.SourcePageType == typeof(QuestionDetailPage):
            //            item = NavView.MenuItems[0] as NavigationViewItem;
            //            break;
            //        case Type t when e.SourcePageType == typeof(ExplorePage):
            //            item = NavView.MenuItems[1] as NavigationViewItem;
            //            break;
            //        case Type t when e.SourcePageType == typeof(TopicPage):
            //            item = NavView.MenuItems[2] as NavigationViewItem;
            //            break;
            //        case Type t when e.SourcePageType == typeof(MePage):
            //            item = NavView.MenuItems[3] as NavigationViewItem;
            //            break;
            //    }
            //    item.IsSelected = true;
            //}
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (!args.IsSettingsSelected)
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
    }
}
