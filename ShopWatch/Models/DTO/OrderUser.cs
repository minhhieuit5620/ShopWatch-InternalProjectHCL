using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWatch.Models.DTO
{
    public class OrderUser
    {
        public List<Order> orders { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}