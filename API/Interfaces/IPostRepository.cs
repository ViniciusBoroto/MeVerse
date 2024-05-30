using API.Data;
using API.Models;
using API.Models.DTOs;

namespace API.Interfaces;

public interface IPostRepository
{
    public Task<Post?> GetByIdAsync(int id);
    public Task<IEnumerable<Post>> GetAllAsync();
    public Task<Post> CreateAsync(Post post);
    public Task<Post> DeleteAsync(Post post);
    public Task<Post> LikeAsync(Post post, User user);
}
