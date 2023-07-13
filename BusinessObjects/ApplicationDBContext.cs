using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObjects
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) : base(opt) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDB"));
        }
        public DbSet<RoleAccount> RoleAccount { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ViewProduct> ViewProduct { get; set; }
        public DbSet<OrdersDetail> OrdersDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleAccount>()
            .HasKey(r => r.roleId);

            modelBuilder.Entity<Category>()
            .HasKey(r => r.categoryId);

            modelBuilder.Entity<ViewProduct>(e =>
            {
                e.HasKey(x => x.viewId);

                e.HasOne(x => x.Product)
                .WithMany(x => x.ViewProducts)
                .HasForeignKey(x => x.productId);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(x => x.productId);

                e.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.categoryId);
            });

            modelBuilder.Entity<Customer>(e =>
            {
                e.HasKey(x => x.customerId);

                e.HasOne(x => x.RoleAccount)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.roleId);
            });

            modelBuilder.Entity<Order>(e =>
            {
                e.HasKey(x => x.orderId);

                e.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.customerId);
            });


            modelBuilder.Entity<OrdersDetail>(e =>
            {
                e.HasKey(x => new { x.orderId, x.productId });

                e.HasOne(x => x.Product)
                .WithMany(x => x.OrdersDetails)
                .HasForeignKey(x => x.productId);

                e.HasOne(x => x.Order)
                .WithMany(x => x.OrdersDetails)
                .HasForeignKey(x => x.orderId);
            });

        }
    }
}
