using System;
using System.Collections.Generic;
using System.Text;

namespace WebShop.Data.Models
{
    public class Cart 
    {
        public int CartId { get; set; }

        public virtual ShopUser ShopUser { get; set; }

        public virtual IEnumerable<CartItem> CartItems { get; set; }
    }
}
