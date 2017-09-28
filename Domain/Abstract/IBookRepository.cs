using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        IEnumerable<Image> Images { get; }

        void SaveBook(Book book);
        Book DeleteBook(int bookId);

        void AddImage(HttpPostedFileBase image, int productID);
        Image DeleteImage(int imageID);
    }
}
