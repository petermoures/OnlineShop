using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class AddCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }
    }
}
