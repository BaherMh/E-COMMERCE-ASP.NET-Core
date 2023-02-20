using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class ProductAttrViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AttrId { get; set; }
        public string Value { get; set; }
        public int? UnitId { get; set; }

        public virtual AttributeViewModel Attr { get; set; }
        public virtual ProductViewModel Product { get; set; }
        public virtual UnitViewModel Unit { get; set; }

        public ProductAttrViewModel()
        {
            //attributesNumber = 1;
            //ProductAttributes = new List<ProductAttrViewModel>();
            //ProductAttributes.ProductId = Id;  
        }
    }
}
