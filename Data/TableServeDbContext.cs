using Microsoft.EntityFrameworkCore;
using TableServe.Api.Models;

namespace TableServe.Api.Data
{
    public class TableServeDbContext : DbContext
    {
        public TableServeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

    }
}
