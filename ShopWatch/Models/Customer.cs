namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDCustomers { get; set; }

        [StringLength(200)]
        public string NameCustomers { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(200)]
        public string Email { get; set; }
    }
}
