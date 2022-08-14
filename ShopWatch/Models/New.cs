namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("New")]
    public partial class New
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NewID { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Describe { get; set; }

        [StringLength(100)]
        public string NewImage { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
