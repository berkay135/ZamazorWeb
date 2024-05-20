﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZamazorWebRazor_Temp.Models
{
    //Create model named "Category"
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display Order Must be between 1-100")]
        public int DisplayOrder { get; set; }

    }
}
