using Domain.Entities;
using System.Collections.Generic;
using System.Web;

namespace Domain.Abstract
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<Image> Images { get; }

        void SaveProduct(Product product);
        Product DeleteProduct(int productId);

        void AddImage(HttpPostedFileBase image, int productID);
        Image DeleteImage(int imageID);
    }
}
