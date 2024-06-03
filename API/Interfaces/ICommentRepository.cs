using API.Models;

namespace API.Interfaces;

public interface ICommentRepository
{
    public Task<Comment?> GetByIdAsync(int id);
    public Task<IEnumerable<Comment>> GetByPostAsync(int postId);
    public Task<IEnumerable<Comment>> GetByCommentAsync(int parentId);
    public Task<Comment?> CreateAsync(Comment comment);
    public Task<Comment?> DeleteAsync(int id);

}
