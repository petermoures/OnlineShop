using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineShop.ViewModel
{
    public class SignInModel
    {
        [Required, MinLength(3), Display(Name = "User Name")]
        public string? UserName { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
