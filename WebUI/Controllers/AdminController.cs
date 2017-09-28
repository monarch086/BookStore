using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        //public ViewResult Edit(int productId)
        //{
        //    Product product = repository.Products.FirstOrDefault(b => b.ProductId == productId);
        //    IEnumerable<Image> images = repository.Images.Where(i => i.ProductID == productId);

        //    return View(new ProductViewModel { Product = product, Images = images });
        //}

        //[HttpPost]
        //public ActionResult Edit(ProductViewModel product, HttpPostedFileBase image = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (image != null)
        //        {
        //            repository.AddImage(image, product.Product.ProductId);
        //        }
        //        repository.SaveProduct(product.Product);
        //        TempData["message"] = string.Format("Изменение информации о товаре \"{0}\" сохранены", product.Product.Name);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(b => b.ProductId == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    repository.AddImage(image, product.ProductId);
                }
                repository.SaveProduct(product);
                TempData["message"] = string.Format("Изменение информации о товаре \"{0}\" сохранены", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
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
    }
}