using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Data
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> items;
        public int total;

        public ShoppingCart()
        {
            items = new List<ShoppingCartItem>();
            total = 0;
        }

        public void add_item(ShoppingCartItem item)
        {
            foreach (ShoppingCartItem it in items)
            {
                if(it.item.Name == item.item.Name)
                {
                    it.amount++;
                    total += item.item.Price;
                    return;
                }
            }
            items.Add(item);
            total += item.item.Price;
        }
        public void remove_item(ShoppingCartItem item)
        {
            foreach (ShoppingCartItem it in items)
            {
                if (it.item.Name == item.item.Name)
                {
                    if (it.amount > 1)
                    {
                        it.amount--;
                        total -= item.item.Price;
                        
                        return;
                    }
                    items.Remove(it);
                    total -= item.item.Price;
                    return;
                }
            }
            return;
        }
    }

    
}
