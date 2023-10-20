using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class FavouriteProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Favourite")]
        public int FavouriteID { get; set; }
        public virtual Favourite? favourite { get; set; }
        [ForeignKey("product")]
        public int prodID { get; set; }
        public virtual product? product { get; set; }


    }
}
