using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class AddproductViewModel
    {
        

        
        [Required, MinLength(10)]
        public string? ProductName { get; set; }
        [Required, RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Dont Write nigative number or Zero")]
        public int ProductPrice { get; set; }
        //public string? Image { get; set; }
        public string? Color { get; set; }
        [Required]
        public bool Available { get; set; }
        //[Required]
       // public string? CategoryName { get; set; }
        [Required, RegularExpression("^[0-9]*$", ErrorMessage = "Dont Write nigative number")]
        public int QuantityToProduct { get; set; }
        public string ImagePath { get; set; }    
        [Required]
        public int CategoryId { get; set; }


    }
}
