using API.Models;
using API.Models.DTOs;

namespace API.Interfaces;

public interface ICommentRepository
{
    public Task<CommentViewModel?> GetByIdAsync(int id);
    public Task<IEnumerable<CommentViewModel?>> GetByPostAsync(int postId);
    public Task<IEnumerable<CommentViewModel?>> GetByCommentAsync(int parentId);
    public Task<CommentViewModel?> CreateAsync(Comment comment);
    public Task<CommentViewModel?> DeleteAsync(int id);

}
