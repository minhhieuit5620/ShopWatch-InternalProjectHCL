namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductInformation")]
    public partial class ProductInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [StringLength(500)]
        public string Brand { get; set; }

        [StringLength(500)]
        public string Origin { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Thickness { get; set; }

        [StringLength(500)]
        public string Shell_material { get; set; }

        [StringLength(500)]
        public string Strap_material { get; set; }

        [StringLength(500)]
        public string Glass_material { get; set; }

        [StringLength(500)]
        public string Water_resistance { get; set; }

        [StringLength(100)]
        public string Sex { get; set; }

        public string MoreImage { get; set; }

        public virtual Product Product { get; set; }
    }
}
