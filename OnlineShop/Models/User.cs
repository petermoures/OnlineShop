using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class User : IdentityUser
    {
        [Required, MaxLength(100)]
        public string ?firstName { get; set; }
        [Required, MaxLength(100)]
        public string ?lastName { get; set; }
        public virtual Cart ?Cart { get; set; }
        public virtual Favourite ? Favourite { get; set; }  
    }
}
