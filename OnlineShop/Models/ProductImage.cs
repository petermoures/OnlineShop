using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? path { get; set; }
        [ForeignKey("product")]
        public int prottId { get; set; }
        public product? product { get; set; }
    }
}
