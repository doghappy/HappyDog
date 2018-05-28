using HappyDog.WindowsUI.Models;
using System.Collections.Generic;

namespace HappyDog.WindowsUI.Services
{
    public static class Configuration
    {
        public static List<Category> Categories => new List<Category>
        {
            new Category { Id = 1, Label = ".Net", Value = ".net", Color = "#7014e8" },
            new Category { Id = 2, Label = "数据库", Value = "db", Color = "#f65314" },
            new Category { Id = 3, Label = "Windows", Value = "windows", Color = "#00a1f1" },
            new Category { Id = 4, Label = "阅读", Value = "read", Color = "#7cbb00" },
            new Category { Id = 5, Label = "随笔", Value = "essays", Color = "#ffbb00" }
        };
    }
}
