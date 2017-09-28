using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IRepository repository;

        public NavController(IRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(int category = 0)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<Category> categories = repository.Categories;

            return PartialView("FlexMenu", categories);
        }

        public PartialViewResult AdminMenu(string section = null)
        {
            ViewBag.Section = section;
            return PartialView();
        }
    }
}