using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<CustomerEntity> Customers { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity() { Id = 1, FirstName = "Андрей", LastName = "Глазев" },
                new CustomerEntity() { Id = 2, FirstName = "Иван", LastName = "Иванов"}
                );
        }
    }
}
