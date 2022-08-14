namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int OrderID { get; set; }

        public int? UserID { get; set; }

        public int? StaffID { get; set; }

        [StringLength(200)]
        public string OrderName { get; set; }

        [StringLength(200)]
        public string OrderEmail { get; set; }

        public int? OrderPhone { get; set; }

        [StringLength(200)]
        public string OrderAddress { get; set; }

        [StringLength(200)]
        public string OrderNote { get; set; }

        public double? TotalMoney { get; set; }

        [Column(TypeName = "date")]
        public DateTime? OrderDate { get; set; }

        public int? Status { get; set; }

        [StringLength(200)]
        public string Payment_method { get; set; }

        public virtual User User { get; set; }
    }
}
