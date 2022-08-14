namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manufacturer")]
    public partial class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Manufacturer_id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public string Described { get; set; }

        [StringLength(100)]
        public string Image { get; set; }

        [StringLength(100)]
        public string Image_brand { get; set; }

        public virtual Product Product { get; set; }
    }
}
