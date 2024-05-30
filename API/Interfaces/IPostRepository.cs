using API.Data;
using API.Models;
using API.Models.DTOs;

namespace API.Interfaces;

public interface IPostRepository
{
    public Task<PostViewModel?> GetByIdAsync(int id, string userId);
    public Task<IEnumerable<PostViewModel>> GetAllAsync(string userId);
    public Task<PostViewModel> CreateAsync(Post post);
    public Task<PostViewModel?> DeleteAsync(int id);
    public Task<PostViewModel> LikeAsync(int postId, User user);
}
