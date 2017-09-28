using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int ProductId { get; set; }

        //[Display(Name="Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название товара")]
        public string Name { get; set; }

        //[Display(Name = "Производитель")]
        [Required(ErrorMessage = "Пожалуйста, укажите производителя")]
        public string Manufacturer { get; set; }

        [DataType(DataType.MultilineText)]
        //[Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание товара")]
        public string Description { get; set; }

        //[Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию товара")]
        public int Category { get; set; }

        //[Display(Name = "Цена")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение цены")]
        public decimal Price { get; set; }
    }
}
