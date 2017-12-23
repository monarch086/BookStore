using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository repository;
        public int pageSize = 6;

        public ProductsController(IRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int category = 0, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = repository.Products
                .Where(b => category == 0 || b.Category == category)
                .OrderBy(product => product.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == 0 ?
                        repository.Products.Count() :
                        repository.Products.Where(product => product.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public ActionResult GetProduct(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                ProductViewModel model = new ProductViewModel
                {
                    Product = product,
                    Category = repository.Categories
                        .FirstOrDefault(c => c.CategoryId == product.Category),
                    Images = repository.Images
                    .Where(i => i.ProductID == productId).ToList() as IEnumerable<string>,
                    ReturnUrl = returnUrl
                };
                return View(model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product not found");
        }

        public string GetImage(int productId)
        {
            Image image = repository.Images.FirstOrDefault(i => i.ProductID == productId);
            if (image != null)
            {
                return image.Path;
            }
            return null;
        }

        //Отображение наиболее покупаемых товаров
        public PartialViewResult Popular()
        {
            IEnumerable<Product> products = repository.Products.Take(3);
            return PartialView(products);
        }
    }
}