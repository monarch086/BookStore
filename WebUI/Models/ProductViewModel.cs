using Domain.Entities;

namespace WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public string[] ImagesPaths { get; set; }
        public Category Category { get; set; }
        public string ReturnUrl { get; set; }
    }
}