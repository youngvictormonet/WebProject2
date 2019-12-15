﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebShop.WebShop.Data.Models;
namespace WebShop.Data.Models
{
    public class CartItem
    {

        public int CartItemId { get; set; }


        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
