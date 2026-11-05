namespace WebApplicationShop.Models
{
    public class ProductFormViewModel
    {
        public Product Product { get; set; } = new Product
        {
            Name = string.Empty,
            Description = string.Empty
        };
        public List<Category> AllCategories { get; set; } = new();
        public List<int> SelectedCategoryIds { get; set; } = new();
    }
}