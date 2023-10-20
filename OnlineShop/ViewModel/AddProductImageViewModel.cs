using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class AddProductImageViewModel
    {
        [Required]
        public int productId { get; set; }
        [Required]
        public string? Path { get; set; }
    }
}
