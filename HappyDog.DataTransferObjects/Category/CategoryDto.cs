using System;
using System.Collections.Generic;
using System.Text;

namespace HappyDog.DataTransferObjects.Category
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Value { get; set; }
        public string Color { get; set; }
    }
}
