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
    public sealed partial class PaginationBar : UserControl, INotifyPropertyChanged
    {
        public PaginationBar()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int pageNumber;
        public int PageNumber
        {
            get => pageNumber;
            set
            {
                pageNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageNumber)));
            }
        }

        private int totalPages;
        public int TotalPages
        {
            get => totalPages;
            set
            {
                totalPages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalPages)));
            }
        }

    }
}
