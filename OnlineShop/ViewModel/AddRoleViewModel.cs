using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
