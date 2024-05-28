using API.Data;
using API.Models;

namespace API.Interfaces;

public interface IPostRepository : IGenericRepository<Post>
{
    public Task<Post> AddLike(int postId, string userId);
    public Task<Post> RemoveLike(int postId, string userId);
}
