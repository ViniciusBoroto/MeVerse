using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class PostRepository : IGenericRepository<Post>

{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Post> CreateAsync(Post entity)
    {
        await _context.Posts.AddAsync(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<Post> DeleteAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<IEnumerable<Post>> GetAllAsync()
    {
        var posts = await  _context.Posts.Take(10).ToListAsync();
        return posts;
    }

    public async Task<Post> GetByIdAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        return post;
    }
}
