using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Favourite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       [ForeignKey("User")]
       public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual IList<FavouriteProduct> favouriteProducts { get; set; }



    }
}
