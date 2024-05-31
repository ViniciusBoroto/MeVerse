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

        // 1:m entre User e Posts
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);

        // 1:m entre User e Comments
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        // m:m entre User Followers e Following
        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany(u => u.Following)
            //Configura A tabela para join
            .UsingEntity<Dictionary<string, object>>(
                "UserFollowers",
                u => u.HasOne<User>().WithMany().HasForeignKey("FollowerId"),
                u => u.HasOne<User>().WithMany().HasForeignKey("FollowingId"),
                j =>
                {
                    j.HasKey("FollowerId", "FollowingId");
                    j.ToTable("UserFollowers");
                });

        // m:m entre Users e LikedPosts
        modelBuilder.Entity<User>()
            .HasMany(u => u.LikedPosts)
            .WithMany(p => p.LikedByUsers)
            //Join
            .UsingEntity<Dictionary<string, object>>(
                "UserLikedPosts",
                u => u.HasOne<Post>().WithMany().HasForeignKey("PostId"),
                p => p.HasOne<User>().WithMany().HasForeignKey("UserId"),
                j =>
                {
                    j.HasKey("UserId", "PostId");
                    j.ToTable("UserLikedPosts");
                });

        // 1:m entre Posts e comenários
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId);

        // m:m entre commentários e Likes
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
