namespace ShopWatch.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rol")]
    public partial class Rol
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public Rol()
        //{
        //    Users = new HashSet<User>();
        //}
        public int Id { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
       // public virtual ICollection<User> Users { get; set; }
    }
}
