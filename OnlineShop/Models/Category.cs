using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CateId { get; set; }
        [Required,MaxLength(100)]
        public string ?CateName { get; set; }
        public virtual ICollection<product> ?Products { get; set; }
    }
}
