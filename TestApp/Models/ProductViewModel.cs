//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TestApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        //[Display(CategoryId = "CategoryId")]
        [Display(Name = "Category")]

        public int CategoryId { get; set; }
        //[Display(Price = "Price")]
        public int Price { get; set; }
        //[Display(Dicount = "Dicount")]
        //public int? Discount { get; set; }
        //[Display(Category = "Category")]
        public string Name { get; set; }
        public string ProductPhoto { get; set; }

        public int? attributesNumber { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public virtual List<ProductAttrViewModel> ProductAttributes { get; set; }

        [Display(Name ="Choose a photo")]
        public IFormFile filePhoto { get; set; }
        public ProductViewModel()
        {
            attributesNumber = 1;
            //ProductAttributes = new List<ProductAttrViewModel>();
            //ProductAttributes.ProductId = Id;  
        }

    }
}
