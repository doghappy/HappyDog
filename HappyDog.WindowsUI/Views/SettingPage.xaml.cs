using System;
using System.ComponentModel;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HappyDog.WindowsUI.Views
{
    public sealed partial class SettingPage : Page, INotifyPropertyChanged
    {
        public SettingPage()
        {
            InitializeComponent();
            var version = Package.Current.Id.Version;
            Version = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
            Theme = App.RootTheme;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private ElementTheme theme;
        public ElementTheme Theme
        {
            get => theme;
            set
            {
                if (theme != value)
                {
                    theme = value;
                    App.RootTheme = theme;
                    App.UpdateTitleBar();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Theme)));
                }
            }
        }
    }
}
