using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int totalQuantity { get; set; }
        [Required]
        public int TotalPrice { get; set; }
        [ForeignKey("User")]
        public string ?UserId { get; set; }
        public virtual User ?User { get; set; }
        //public virtual IList<product> Productss { get; set; }   
        public virtual IList<CartProduct> CartProducts { get; set; }   
    }
}
