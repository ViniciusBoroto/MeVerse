using API.Interfaces;
using API.Models;
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

    public async Task<Post> CreateAsync(string userId, string text)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var post = new Post { User = user, UserId = userId, Text = text };
        await _posts.AddAsync(post);
        _context.SaveChanges();
        return post;
    }

    public async Task<Post> DeleteAsync(int id)
    {
        var post = await _posts.FindAsync(id);
        _posts.Remove(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _posts.Include(p => p.User).Take(10).ToListAsync();
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        return await _posts.Include(p => p.User).FirstOrDefaultAsync(p => p.PostId == id);
    }

    public async Task<Post> AddLikeAsync(Post post, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (post.LikedByUsers.Contains(user))
        {
            post.LikedByUsers.Remove(user);
            return (false, post);
        }

        post.LikedByUsers.Add(user);
        _posts.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return (true, post);
    }

    public async Task<Post> RemoveLikeAsync(Post post, User user)
    {
        post.LikedByUsers.Remove(user);
        _posts.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return post;
    }
}
