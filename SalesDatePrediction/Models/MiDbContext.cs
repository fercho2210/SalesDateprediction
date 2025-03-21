using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Models;

public class MiDbContext : DbContext
{
    public MiDbContext(DbContextOptions<MiDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Shipper> Shippers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Configuración explícita del esquema y la clave primaria para Customer
        modelBuilder.Entity<Customer>()
            .ToTable("Customers", schema: "Sales")
            .HasKey(c => c.CustId); 

        modelBuilder.Entity<Employee>()
            .ToTable("Employees", schema: "HR")
            .HasKey(e => e.Empid);

        modelBuilder.Entity<Order>()
        .HasOne(o => o.Customer)
        .WithMany()
        .HasForeignKey(o => o.CustId);

        modelBuilder.Entity<Order>()
        .HasOne(o => o.Employee)
        .WithMany()
        .HasForeignKey(o => o.EmpId);

        modelBuilder.Entity<Order>()
            .ToTable("Orders", schema: "Sales")
            .HasKey(e => e.OrderId);

        // Configuración de la clave primaria compuesta para OrderDetail
        modelBuilder.Entity<OrderDetail>()
              .HasKey(od => new { od.OrderId, od.ProductId });
        // Configuración de la relación con Order
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);
        // Configuración de la relación con Product
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany()
            .HasForeignKey(od => od.ProductId);

        modelBuilder.Entity<OrderDetail>()
        .ToTable("OrderDetails", schema: "Sales");

        modelBuilder.Entity<Shipper>()
            .ToTable("Shippers", schema: "Sales")
            .HasKey(e => e.ShipperId);

        modelBuilder.Entity<Product>()
            .ToTable("Products", schema: "Production")
            .HasKey(e => e.ProductId);

        modelBuilder.Entity<Category>()
            .ToTable("Categories", schema: "Production")
            .HasKey(e => e.CategoryId);

        modelBuilder.Entity<Supplier>()
            .ToTable("Suppliers", schema: "Production")
            .HasKey(e => e.Supplierid);


        base.OnModelCreating(modelBuilder);
    }
}