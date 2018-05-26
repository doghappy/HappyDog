using HappyDog.WindowsUI.Common;
using HappyDog.WindowsUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private IncrementalCollection<Article> articles;
        public IncrementalCollection<Article> Articles
        {
            get => articles;
            set
            {
                articles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Articles)));
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }
    }
}
