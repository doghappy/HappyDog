using HappyDog.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HappyDog.Domain.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Display(Name = "分类ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "标签")]
        [StringLength(20, MinimumLength = 1)]
        public string Label { get; set; }

        [Required]
        [Display(Name = "值")]
        [StringLength(20, MinimumLength = 1)]
        public string Value { get; set; }

        [Required]
        [Display(Name = "颜色")]
        public string Color { get; set; }

        [Required]
        [Display(Name = "图标类样式")]
        public string IconClass { get; set; }

        [Required]
        [Display(Name = "状态")]
        public BaseState State { get; set; }

        public List<Article> Article { get; set; }
    }
}
