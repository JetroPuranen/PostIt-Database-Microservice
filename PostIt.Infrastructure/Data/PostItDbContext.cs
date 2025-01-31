using Microsoft.EntityFrameworkCore;
using PostIt.Domain.Entities;


namespace PostIt.Infrastructure.Data
{
    public class PostItDbContext : DbContext
    {
        public PostItDbContext(DbContextOptions<PostItDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<UserFollowers> UserFollowers { get; set; }

        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Users
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.SurName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.HomeAddress)
                    .HasMaxLength(200);

                entity.Property(u => u.ProfilePicture)
                    .HasColumnType("varbinary(max)");

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(256);

                // Fix: Configure many-to-many relationship through UserFollowers
                entity.HasMany(u => u.Followers)
                    .WithOne(f => f.Following)
                    .HasForeignKey(f => f.FollowingId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.Following)
                    .WithOne(f => f.Follower)
                    .HasForeignKey(f => f.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.Posts)
                      .WithOne()
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure UserFollowers
            modelBuilder.Entity<UserFollowers>(entity =>
            {
                entity.HasKey(uf => new { uf.FollowerId, uf.FollowingId });

                entity.HasOne(uf => uf.Follower)
                    .WithMany(u => u.Following)
                    .HasForeignKey(uf => uf.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(uf => uf.Following)
                    .WithMany(u => u.Followers)
                    .HasForeignKey(uf => uf.FollowingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Posts
            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Caption).IsRequired().HasMaxLength(1000);

                entity.HasMany(p => p.Comments)
                      .WithOne(c => c.Post)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(p => p.Likes)
                      .WithOne(l => l.Post)
                      .HasForeignKey(l => l.PostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure PostComment
            modelBuilder.Entity<PostComment>(entity =>
            {
                entity.HasKey(pc => pc.Id);
                entity.Property(pc => pc.Comment).IsRequired().HasMaxLength(1000);

                entity.HasOne(pc => pc.User)
                      .WithMany()
                      .HasForeignKey(pc => pc.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure PostLike
            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.HasKey(pl => pl.Id);

                entity.HasOne(pl => pl.User)
                      .WithMany()
                      .HasForeignKey(pl => pl.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
