namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ImportBill")]
    public partial class ImportBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDBill { get; set; }

        public int? IDSupplier { get; set; }

        public int? IDProduct { get; set; }

        public int? IDStaff { get; set; }

        [StringLength(200)]
        public string NameShipper { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DateImport { get; set; }

        public double? TotalMoney { get; set; }

        public virtual Product Product { get; set; }

        public virtual Staff Staff { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual ImportBillDetail ImportBillDetail { get; set; }
    }
}
