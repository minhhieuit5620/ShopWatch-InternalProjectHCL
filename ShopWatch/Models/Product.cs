namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ImportBills = new HashSet<ImportBill>();
        }

        public int ProductID { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        public string ProductImage { get; set; }

        public double? Price { get; set; }

        public double? PriceOld { get; set; }

        public double? ImportPrice { get; set; }

        public int? Manufacturer_id { get; set; }

        public int? Amount { get; set; }

        public int? Category_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBill> ImportBills { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ProductInformation ProductInformation { get; set; }
    }
}
