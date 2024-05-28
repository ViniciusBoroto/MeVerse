using API.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Post> _posts;
    public PostRepository(AppDbContext context) : base(context)
    {
        _context = context;
        _posts = _context.Set<Post>();
    }

    public async Task<Post> AddLike(Post post, User user)
    {
        post.LikedByUsers.Add(user);
        _posts.Entry(post).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return post;
    }

    public Task<Post> RemoveLike(int postId, string userId)
    {
        throw new NotImplementedException();
    }
}
