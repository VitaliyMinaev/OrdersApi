using Microsoft.EntityFrameworkCore;
using OrdersApi.Entities;

namespace OrdersApi.Persistence;

public class ApplicationDatabaseContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    
    public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options)
    {  }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}