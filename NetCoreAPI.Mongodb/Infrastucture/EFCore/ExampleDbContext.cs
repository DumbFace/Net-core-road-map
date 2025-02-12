using Domain.EFCore.Entites;
using Infrastucture.Domain.EFCore.Entites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastucture.EFCore
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    SetSoftDeleteFilter(modelBuilder, entityType.ClrType);
                }
            }
        }

        private static void SetSoftDeleteFilter(ModelBuilder modelBuilder, Type entityType)
        {
            MethodInfo method = typeof(EFFilterExtensions)
                .GetMethod(
                    nameof(EFFilterExtensions.SetSoftDeleteFilter),
                    BindingFlags.Public |
                    BindingFlags.Static)
                .MakeGenericMethod(entityType);

            method.Invoke(null, new object[] { modelBuilder });
        }

    }

    public static class EFFilterExtensions
    {
        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
           where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
