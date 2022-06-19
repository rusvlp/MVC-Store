using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Entities
{
    public class Part
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Категория")]
        public string Category { get; set; }

        [Display(Name = "Цена (руб)")]
        public decimal Price { get; set; }

        [Display(Name = "Количество на складе")]
        public int? Quantity { get; set; }
    }
}
