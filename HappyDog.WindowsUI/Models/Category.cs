using HappyDog.WindowsUI.Enums;
using System.Collections.Generic;

namespace HappyDog.WindowsUI.Models
{

    public class Category
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }
        public BaseState State { get; set; }
        public List<Article> Article { get; set; }
    }
}
