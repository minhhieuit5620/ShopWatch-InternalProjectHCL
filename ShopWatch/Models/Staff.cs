namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            ImportBills = new HashSet<ImportBill>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDStaff { get; set; }

        [StringLength(100)]
        public string NameStaff { get; set; }

        public DateTime? Birthday { get; set; }

        [StringLength(10)]
        public string Gender { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ImportBill> ImportBills { get; set; }
    }
}
