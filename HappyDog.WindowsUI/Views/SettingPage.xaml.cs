using HappyDog.WindowsUI.Common;
using System;
using System.ComponentModel;
using System.Linq;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
            var version = Package.Current.Id.Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string tag = App.RootTheme.ToString();
            var rBtns = this.GetChildren<RadioButton>();
            foreach (var item in rBtns)
            {
                if (item.Tag.ToString()==tag)
                {
                    item.IsChecked = true;
                }
                item.Checked += OnThemeChanged;
            }
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
            App.UpdateTitleBar();
            Configuration.CachedPages.ForEach(p => p.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Disabled);
            Configuration.CachedPages.Clear();
        }
    }
}
