using System.Web.Mvc;

namespace Domain.Entities
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }
}
