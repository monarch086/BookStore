using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Domain.Concrete
{
    public class EFRepository : IRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return context.Categories;
            }
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Manufacturer = product.Manufacturer;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = product.Category;
                    dbEntry.Price = product.Price;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public IEnumerable<Image> Images
        {
            get
            {
                return context.Images;
            }
        }

        public void AddImage(HttpPostedFileBase image, int productID)
        {
            //Copy file
            string fileName = "~/Files/" + System.IO.Path.GetFileName(image.FileName);
            image.SaveAs(HostingEnvironment.MapPath(fileName));
            //Save to DB
            context.Images.Add(new Image { ProductID = productID, Path = fileName });
            context.SaveChanges();
        }

        public Image DeleteImage(int imageID)
        {
            throw new NotImplementedException();
        }
    }
}
