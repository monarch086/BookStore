using System;
using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IRepository repository;
        
        public AdminController(IRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ActionResult Edit(int productId)
        {
            var product = repository.Products.FirstOrDefault(b => b.ProductId == productId);
            if (product != null)
            {
                ProductEditViewModel model = new ProductEditViewModel
                {
                    Product = product,
                    Images = repository.Images
                        .Where(i => i.ProductID == productId).ToArray(),
                    Category = repository.Categories
                        .FirstOrDefault(c => c.CategoryId == product.Category),
                    Categories = repository.Categories.ToList()
                };
                return View(model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product not found");
        }

        [HttpPost]
        public ActionResult Edit(ProductEditViewModel product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    repository.AddImage(image, product.Product.ProductId);
                }
                repository.SaveProduct(product.Product);
                TempData["message"] = string.Format("Изменение информации о товаре \"{0}\" сохранены", product.Product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new ProductEditViewModel());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveImage(int imageId)
        {
            if (repository.Images.FirstOrDefault(x => x.ID == imageId) != null)
            {
                repository.DeleteImage(imageId);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Image not found");
        }
    }
}