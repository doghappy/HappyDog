using HappyDog.WindowsUI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace HappyDog.WindowsUI.Common
{
    public static class Configuration
    {
        static Configuration()
        {
            CachedPages = new List<Page>();
            Categories = new ObservableCollection<Category>
            {
                new Category { Id = 1, Label = ".Net", Value = ".net", Color = "#7014e8" },
                new Category { Id = 2, Label = "数据库", Value = "db", Color = "#f65314" },
                new Category { Id = 3, Label = "Windows", Value = "windows", Color = "#00a1f1" },
                new Category { Id = 4, Label = "阅读", Value = "read", Color = "#7cbb00" },
                new Category { Id = 5, Label = "随笔", Value = "essays", Color = "#ffbb00" }
            };
        }

        public static ObservableCollection<Category> Categories { get; }

        static List<Page> CachedPages { get; }

        public static bool IsAuthorized { get; set; }

        public static void ClearCache()
        {
            CachedPages.ForEach(p => p.NavigationCacheMode = NavigationCacheMode.Disabled);
            CachedPages.Clear();
        }

        public static void AddPageCache(Page page)
        {
            if (!CachedPages.Any(a => a.GetType() == page.GetType()))
            {
                CachedPages.Add(page);
            }
        }
    }
}
