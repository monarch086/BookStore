using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Web;
using System.Web.Hosting;

namespace Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Book> Books
        {
            get
            {
                return context.Books;
            }
        }

        public void SaveBook(Book book)
        {
            if (book.BookId == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookId);
                if (dbEntry != null)
                {
                    dbEntry.Name = book.Name;
                    dbEntry.Author = book.Author;
                    dbEntry.Description = book.Description;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Price = book.Price;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(int bookId)
        {
            Book dbEntry = context.Books.Find(bookId);
            if(dbEntry != null)
            {
                context.Books.Remove(dbEntry);
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
