using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Controllers;
using WebUI.Models;

namespace UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Books()
        {
            //Организация
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { BookId = 1, Name = "Book1"},
                new Book { BookId = 2, Name = "Book2"},
                new Book { BookId = 3, Name = "Book3"},
                new Book { BookId = 4, Name = "Book4"},
                new Book { BookId = 5, Name = "Book5"},
            });

            AdminController controller = new AdminController(mock.Object);

            //Действие
            List<Book> result = ((IEnumerable<Book>)controller.Index().ViewData.Model).ToList();

            //Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual(result[0].Name, "Book1");
            Assert.AreEqual(result[1].Name, "Book2");
        }

        [TestMethod]
        public void Can_Edit_Book()
        {
            //Организация
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { BookId = 1, Name = "Book1"},
                new Book { BookId = 2, Name = "Book2"},
                new Book { BookId = 3, Name = "Book3"},
                new Book { BookId = 4, Name = "Book4"},
                new Book { BookId = 5, Name = "Book5"},
            });

            AdminController controller = new AdminController(mock.Object);

            //Действие
            Book book1 = controller.Edit(1).ViewData.Model as Book;
            Book book2 = controller.Edit(2).ViewData.Model as Book;
            Book book3 = controller.Edit(3).ViewData.Model as Book;

            //Утверждение
            Assert.AreEqual(1, book1.BookId);
            Assert.AreEqual(2, book2.BookId);
            Assert.AreEqual(3, book3.BookId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Book()
        {
            //Организация
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book { BookId = 1, Name = "Book1"},
                new Book { BookId = 2, Name = "Book2"},
                new Book { BookId = 3, Name = "Book3"},
                new Book { BookId = 4, Name = "Book4"},
                new Book { BookId = 5, Name = "Book5"},
            });

            AdminController controller = new AdminController(mock.Object);

            //Действие
            Book book1 = controller.Edit(7).ViewData.Model as Book;

            //Утверждение
            Assert.IsNull(book1);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            //Организация
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController controller = new AdminController(mock.Object);
            Book book = new Book { Name = "Test" };

            //Действие
            ActionResult result = controller.Edit(book);

            //Утверждение
            mock.Verify(m => m.SaveBook(book));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Организация - создание имитированного хранилища данных
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Организация - создание объекта Book
            Book book = new Book { Name = "Test" };

            // Организация - добавление ошибки в состояние модели
            controller.ModelState.AddModelError("error", "error");

            // Действие - попытка сохранения товара
            ActionResult result = controller.Edit(book);

            // Утверждение - проверка того, что обращение к хранилищу НЕ производится 
            mock.Verify(m => m.SaveBook(It.IsAny<Book>()), Times.Never());

            // Утверждение - проверка типа результата метода
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
