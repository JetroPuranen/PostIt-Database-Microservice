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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users entity configuration
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

                entity.HasMany(u => u.Followers)
                    .WithMany(u => u.Following)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserFollowers",
                        j => j
                            .HasOne<Users>()
                            .WithMany()
                            .HasForeignKey("FollowerId")
                            .OnDelete(DeleteBehavior.Restrict),
                        j => j
                            .HasOne<Users>()
                            .WithMany()
                            .HasForeignKey("FollowingId")
                            .OnDelete(DeleteBehavior.Restrict));

                entity.HasMany(u => u.Posts)
                    .WithOne()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Caption)
                    .HasMaxLength(500);

                entity.Property(p => p.ImageData)
                    .IsRequired();

                
                entity.HasOne<Users>()
                    .WithMany(u => u.Posts)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
