using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProductViewModel
    {
        public Book Book { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Image> Images { get; set; }
    }
}