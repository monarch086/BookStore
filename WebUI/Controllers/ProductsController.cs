using System.Collections;
using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using AutoMapper;
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
            var products = repository.Products
                .Where(b => category == 0 || b.Category == category)
                .OrderBy(product => product.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            var productsDTO = Mapper.Map<IEnumerable<Product>, List<ProductSmallViewModel>>(products);

            ProductListViewModel model = new ProductListViewModel
            {
                Products = productsDTO,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == 0 ?
                        repository.Products.Count() :
                        repository.Products.Count(product => product.Category == category)
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
                    ImagesPaths = repository.Images
                    .Where(i => i.ProductID == productId).Select(i => i.Path).ToArray(),
                    ReturnUrl = returnUrl
                };
                return View(model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product not found");
        }

        //Отображение наиболее покупаемых товаров
        public PartialViewResult Popular()
        {
            var products = repository.Products.Take(3);
            var productsDTO = Mapper.Map<IEnumerable<Product>, List<ProductSmallViewModel>>(products);
            return PartialView(productsDTO);
        }
    }
}