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
        public BaseStatus State { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Category category)
            {
                return category.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
