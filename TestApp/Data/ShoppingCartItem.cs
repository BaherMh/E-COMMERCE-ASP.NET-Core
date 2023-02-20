using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Data
{
    public class ShoppingCartItem
    {
        public ProductViewModel item;
        public int amount;

        public ShoppingCartItem()
        {
            item = new ProductViewModel();
            amount = 0;
        }

        public ShoppingCartItem(ProductViewModel prod)
        {
            item = prod;
            amount = 1;
        }
    }
}
