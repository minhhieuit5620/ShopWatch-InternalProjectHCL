using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ShopWatch.Models
{
    public partial class ShopEntities : DbContext
    {
        public ShopEntities()
            : base("name=ShopEntities")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<ImportBill> ImportBills { get; set; }
        public virtual DbSet<ImportBillDetail> ImportBillDetails { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<New> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInformation> ProductInformations { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImportBill>()
                .HasOptional(e => e.ImportBillDetail)
                .WithRequired(e => e.ImportBill);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ImportBills)
                .WithOptional(e => e.Product)
                .HasForeignKey(e => e.IDProduct);

            modelBuilder.Entity<Product>()
                .HasOptional(e => e.Manufacturer)
                .WithRequired(e => e.Product);

            modelBuilder.Entity<Product>()
                .HasOptional(e => e.ProductInformation)
                .WithRequired(e => e.Product);
        }
    }
}
