//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TestApp.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int? attributesNumber { get; set; }
        public string PhotoUrl { get; set; }

        [Display(Name = "Choose a photo")]
        public IFormFile filePhoto { get; set; }
        public virtual List<CategoryAttrViewModel> CategoryAttributes { get; set; }
        public virtual List<ProductViewModel> CategoryProducts { get; set; }
        public CategoryViewModel()
        {
            attributesNumber = 1;
        }

    }
}
