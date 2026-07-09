using Microsoft.EntityFrameworkCore;
using TableServe.Api.Models;

namespace TableServe.Api.Data
{
    public class TableServeDbContext : DbContext
    {
        public TableServeDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Staff> Staffs { get; set; }

    }
}
