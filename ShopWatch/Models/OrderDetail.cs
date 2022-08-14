namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public int ID { get; set; }

        public int? OrderID { get; set; }

        public int? ProductID { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        public double? Total { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? Status { get; set; }
    }
}
