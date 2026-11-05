using System.ComponentModel.DataAnnotations;

namespace WebApplicationShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [StringLength(50, ErrorMessage = "Не более 50 символов")]
        public required string Name { get; set; }
        public List<Product> Products { get; set; } = [];
    }
}