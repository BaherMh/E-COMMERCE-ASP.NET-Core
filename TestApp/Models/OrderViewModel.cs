using AppDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public byte IsPaid { get; set; }
        public int ShippingId { get; set; }
        public int Price { get; set; }
        public string State { get; set; }

        public virtual Shipping Shipping { get; set; }
        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
