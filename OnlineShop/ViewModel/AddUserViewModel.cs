using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OnlineShop.ViewModel
{
    public class AddUserViewModel
    {
        [Required, MinLength(3), Display(Name = "User Name")]
        public string? UserName { get; set; }
        [Required, MinLength(6), DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required, MinLength(3), Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required, MinLength(6), EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(3), Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Required, Compare("Password"), DataType(DataType.Password), Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }

       // public string Role { get; set; }
    }
}
