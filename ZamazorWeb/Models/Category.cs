using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZamazorWeb.Models
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
        [Range(1,100)]
        public int DisplayOrder { get; set; }

    }
}
