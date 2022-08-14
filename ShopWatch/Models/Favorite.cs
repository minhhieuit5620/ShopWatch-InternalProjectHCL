namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Favorite")]
    public partial class Favorite
    {
        [Key]
        public int Favorite_Id { get; set; }

        public int? Product_Id { get; set; }

        [StringLength(500)]
        public string NameProduct { get; set; }

        [StringLength(100)]
        public string ProductImage { get; set; }

        public double? Price { get; set; }

        public int? Manufacturer_id { get; set; }

        public int? Category_id { get; set; }

        public int? User_id { get; set; }
    }
}
