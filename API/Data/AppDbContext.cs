using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace API.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure one-to-many relationship between ApplicationUser and posts
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        // Configure one-to-many relationship between ApplicationUser and comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        // Configure many-to-many relationship between users for followers and following
        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following)
            .UsingEntity<Dictionary<string, object>>(
                "UserFollowers",
                u => u.HasOne<User>().WithMany().HasForeignKey("FollowerId"),
                u => u.HasOne<User>().WithMany().HasForeignKey("FollowingId"),
                j =>
                {
                    j.HasKey("FollowerId", "FollowingId");
                    j.ToTable("UserFollowers");
                });

        // Configure one-to-many relationship between user and posts
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        // Configure many-to-many relationship between users and liked posts
        modelBuilder.Entity<User>()
            .HasMany(u => u.LikedPosts)
            .WithMany(p => p.LikedByUsers)
            .UsingEntity<Dictionary<string, object>>(
                "UserLikedPosts",
                u => u.HasOne<Post>().WithMany().HasForeignKey("PostId"),
                p => p.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "PostId");
                    j.ToTable("UserLikedPosts");
                });

        // Configure one-to-many relationship between post and comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId);

        // Configure many-to-many relationship between users and liked comments
        modelBuilder.Entity<User>()
            .HasMany(u => u.LikedComments)
            .WithMany(c => c.LikedByUsers)
            .UsingEntity<Dictionary<string, object>>(
                "UserLikedComments",
                u => u.HasOne<Comment>().WithMany().HasForeignKey("CommentId"),
                c => c.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "CommentId");
                    j.ToTable("UserLikedComments");
                });
    }


}
