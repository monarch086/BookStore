using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<ProductSmallViewModel> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentCategory { get; set; }
    }
}