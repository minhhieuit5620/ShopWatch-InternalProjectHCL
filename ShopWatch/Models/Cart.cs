using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.Models
{
    public class Cart
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice {
            get { return Quantity * Price; }
        
        }
    }
}