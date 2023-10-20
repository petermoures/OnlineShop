using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewModel
{
    public class AddOrderViewModel
    {
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required,DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [Required, MaxLength(200)]
        public string? Address { get; set; }
        [Required, MaxLength(200),DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        //[Required,DataType(DataType.DateTime)]
        //public DateTime OrderDate { get; set; }
    }
}
