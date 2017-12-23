using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<string> Images { get; set; }
        public Category Category { get; set; }
        public string ReturnUrl { get; set; }
    }
}