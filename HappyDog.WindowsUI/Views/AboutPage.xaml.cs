﻿using System;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            var version = Package.Current.Id.Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private async void EmailIcon_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("mailto:hero_wong@outlook.com"));
        }

        private async void GithubIcon_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/doghappy"));
        }

        private async void DogHappy_Click(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("http://doghappy.wang"));
        }

        public string Version { get; }

        private void OnThemeChanged(object sender, RoutedEventArgs e)
        {
            var btn = sender as RadioButton;
            ElementTheme theme = Enum.Parse<ElementTheme>(btn.Tag.ToString());
            App.RootTheme = theme;

            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (theme == ElementTheme.Default)
            {
                if (Application.Current.RequestedTheme == ApplicationTheme.Light)
                {
                    titleBar.ButtonForegroundColor = Colors.Black;
                }
                else
                {
                    titleBar.ButtonForegroundColor = Colors.White;
                }
            }
            else if (theme == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Colors.Black;
            }
            else if (theme == ElementTheme.Dark)
            {
                titleBar.ButtonForegroundColor = Colors.White;
            }
        }
    }
}
