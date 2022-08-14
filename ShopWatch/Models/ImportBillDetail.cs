namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportBillDetail")]
    public partial class ImportBillDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDBill { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public int? Quantity { get; set; }

        public double? UnitPrice { get; set; }

        public virtual ImportBill ImportBill { get; set; }
    }
}
