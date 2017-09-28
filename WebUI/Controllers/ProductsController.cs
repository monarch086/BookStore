using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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