using API.Data;
using API.Models;

namespace API.Interfaces;

public interface IPostRepository
{
    public Task<Post> GetByIdAsync(int id);
    public Task<IEnumerable<Post>> GetAllAsync();
    public Task<Post> CreateAsync(string userid, string text);
    public Task<Post> DeleteAsync(int id);
    public Task<Post> AddLikeAsync(Post post, string userId);
    public Task<Post> RemoveLikeAsync(Post post, User user);
}
