using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}