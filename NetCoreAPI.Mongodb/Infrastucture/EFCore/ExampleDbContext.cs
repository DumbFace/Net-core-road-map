using Infrastucture.Domain.EFCore.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.EFCore
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Employee>();
        }
    }
}
