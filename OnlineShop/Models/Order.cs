using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required, MaxLength(200)]
        public string? Address { get; set; }
        [Required, MaxLength(200)]
        public string? Email { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }

    }
}
