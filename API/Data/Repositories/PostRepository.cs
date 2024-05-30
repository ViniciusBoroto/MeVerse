using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Post> _posts;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public PostRepository(AppDbContext context, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        _posts = _context.Set<Post>();
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        return await _posts.Include(p => p.User).Include(p => p.LikedByUsers).Take(10).ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _posts.Include(p => p.User).Include(p => p.LikedByUsers).FirstOrDefaultAsync(p => p.PostId == id);
    }

    public async Task<Post> CreateAsync(Post post)
    {
        await _posts.AddAsync(post);
        _context.SaveChanges();
        return post;
    }

    public async Task<Post> DeleteAsync(Post post)
    {
        _posts.Remove(post);
        await _context.SaveChangesAsync();
        return post;
    }


    public async Task<Post> LikeAsync(Post post, User user)
    {
        if (post.LikedByUsers.Contains(user))
            post.LikedByUsers.Remove(user);
        else
            post.LikedByUsers.Add(user);
        _posts.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return post;
    }
}
