using Domain.EFCore.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Infrastucture.EFCore
{
    public class StackOverflowDBContext : DbContext
    {
        public StackOverflowDBContext(DbContextOptions<StackOverflowDBContext> options) : base(options)
        {
        }
        public virtual DbSet<Badge> Badges { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<LinkType> LinkTypes { get; set; }

        public DbSet<PostLink> PostLinks { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostType> PostTypes { get; set; }

        public virtual DbSet<User> Users { get; set; }

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
            modelBuilder.Entity<Badge>().Ignore(badge => badge.User);
            //modelBuilder.Entity<User>().Ignore(user => user.Badges);
            modelBuilder.Entity<Comment>().Ignore(comment => comment.User).Ignore(comment => comment.Post);
            modelBuilder.Entity<Post>().Ignore(post => post.PostLink);
            modelBuilder.Entity<Post>().Ignore(post => post.Comments);
            modelBuilder.Entity<Post>().Ignore(post => post.Votes);
            modelBuilder.Entity<Vote>().Ignore(vote => vote.VoteType);
        }
    }
}
