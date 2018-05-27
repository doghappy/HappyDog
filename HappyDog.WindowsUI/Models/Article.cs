using HappyDog.WindowsUI.Enums;
using HappyDog.WindowsUI.Views;
using System;

namespace HappyDog.WindowsUI.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateTime { get; set; }
        public int ViewCount { get; set; }
        public BaseState State { get; set; }
        public Category Category { get; set; }
    }
}
