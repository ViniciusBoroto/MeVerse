using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Post> _posts;
    private readonly UserManager<User> _userManager;
    public PostRepository(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _posts = _context.Set<Post>();
        _userManager = userManager;
    }
    public async Task<IEnumerable<PostViewModel>> GetAllAsync(string userId)
    {
        return await _posts
            .AsNoTracking()
            .Take(10)
            .Select(p => new PostViewModel
            {
                User = new UserViewModel
                {
                    Id = p.UserId,
                    ProfileImagePath = p.User.ProfileImagePath,
                    UserName = p.User.UserName
                },
                PostId = p.PostId,
                Text = p.Text,
                LikedByUsers = p.LikedByUsers.Select(u =>
                    new UserViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        ProfileImagePath = u.ProfileImagePath
                    }).ToList(),
                UserLiked = p.LikedByUsers.Any(l => l.Id == userId),
                Likes = p.LikedByUsers.Count()  
            }).ToListAsync();
    }

    public async Task<PostViewModel?> GetByIdAsync(int id, string userId)
    {
        var post = await _posts
            .AsNoTracking()
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.PostId == id);
        if (post is null) return null;
        return await mapPost(post, userId);
    }

    public async Task<PostViewModel> CreateAsync(Post post)
    {
        await _posts.AddAsync(post);
        _context.SaveChanges();
        return await mapPost(post, post.User.Id);
    }

    public async Task<PostViewModel?> DeleteAsync(int id)
    {
        var post = await _posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.PostId == id);
        if (post is null) return null;
        _posts.Remove(post);
        await _context.SaveChangesAsync();
        return await mapPost(post, post.User.Id);
    }


    public async Task<PostViewModel> LikeAsync(int postId, User user)
    {
        var post = await _posts
            .Include(p => p.LikedByUsers)
            .FirstOrDefaultAsync(p => p.PostId == postId);

        if (post.LikedByUsers.Any(u => u.Id == user.Id))
        {
            post.LikedByUsers.Remove(user);
        }
        else
        {
            post.LikedByUsers.Add(user);
        }
        _posts.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return await mapPost(post, user.Id);
    }

    private async Task<PostViewModel> mapPost (Post post, string userId)
    {
        return new PostViewModel
        {
            PostId = post.PostId,
            User = new UserViewModel
            {
                Id = post.User.Id,
                UserName = post.User.UserName,
                ProfileImagePath = post.User.UserName
            },
            LikedByUsers = post.LikedByUsers.Select(u =>
                new UserViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    ProfileImagePath = u.ProfileImagePath
                }
                    ).ToList(),
            Likes = post.LikedByUsers.Count(),
            Text = post.Text,
            UserLiked = post.LikedByUsers.Any(l => l.Id == userId)
        };
    }
}
