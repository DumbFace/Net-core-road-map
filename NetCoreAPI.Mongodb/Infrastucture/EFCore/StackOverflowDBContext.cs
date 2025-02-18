using Domain.EFCore.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;


namespace Infrastucture.EFCore
{
    public class StackOverflowDBContext : DbContext
    {
        public StackOverflowDBContext(DbContextOptions<StackOverflowDBContext> options) : base(options)
        {
        }
        public DbSet<Badge> Badges { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<LinkType> LinkTypes { get; set; }

        public DbSet<PostLink> PostLinks { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostType> PostTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<VoteType> VoteTypes { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyAttributeConvention));
            configurationBuilder.Conventions.Remove<ForeignKeyIndexConvention>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //new EntityLinkTypeConfig().Configure(modelBuilder.Entity<LinkType>());
            //modelBuilder.Entity<Badge>().ToTable("Badges", badge => badge.ExcludeFromMigrations(false));
            //modelBuilder.Entity<Comment>().ToTable("Comments", Comment => Comment.ExcludeFromMigrations(true));
            //modelBuilder.Entity<LinkType>().ToTable("LinkTypes", LinkType => LinkType.ExcludeFromMigrations(true));
            //modelBuilder.Entity<PostLink>().ToTable("PostLinks", PostLink => PostLink.ExcludeFromMigrations(true));
            //modelBuilder.Entity<Post>().ToTable("Posts", Post => Post.ExcludeFromMigrations(true));
            //modelBuilder.Entity<PostType>().ToTable("PostTypes", PostType => PostType.ExcludeFromMigrations(true));
            //modelBuilder.Entity<User>().ToTable("Users", User => User.ExcludeFromMigrations(true));
            //modelBuilder.Entity<Vote>().ToTable("Votes", Vote => Vote.ExcludeFromMigrations(true));
            //modelBuilder.Entity<VoteType>().ToTable("VoteTypes", VoteType => VoteType.ExcludeFromMigrations(true));
            //modelBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
            //modelBuilder.ApplyConfiguration<Post>()

        }
    }
}
