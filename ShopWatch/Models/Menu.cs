namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MenuID { get; set; }

        [StringLength(100)]
        public string Text { get; set; }

        [StringLength(200)]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        public int? Status { get; set; }
    }
}
