using System.ComponentModel.DataAnnotations;

namespace WebApplicationShop.Models
{   
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название")]
        [StringLength(100, ErrorMessage = "Не более 100 символов")]
        public required string Name { get; set; }
        [Range(0.01, 10000000, ErrorMessage = "Цена должна быть положительной")]
        public decimal Price { get; set; }
        [StringLength(500)]
        public required string Description { get; set; }
        public List<Category> Categories { get; set; } = [];
    }
}