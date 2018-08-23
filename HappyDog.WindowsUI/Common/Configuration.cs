using HappyDog.WindowsUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Common
{
    public static class Configuration
    {
        public static ObservableCollection<Category> Categories => new ObservableCollection<Category>
        {
            new Category { Id = 1, Label = ".Net", Value = ".net", Color = "#7014e8" },
            new Category { Id = 2, Label = "数据库", Value = "db", Color = "#f65314" },
            new Category { Id = 3, Label = "Windows", Value = "windows", Color = "#00a1f1" },
            new Category { Id = 4, Label = "阅读", Value = "read", Color = "#7cbb00" },
            new Category { Id = 5, Label = "随笔", Value = "essays", Color = "#ffbb00" }
        };

        public static NavigationCacheMode ArticlePageCache = NavigationCacheMode.Enabled;
    }
}
