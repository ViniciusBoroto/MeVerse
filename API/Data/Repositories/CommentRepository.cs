using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class CommentRepository : ICommentRepository
{
    AppDbContext _context;
    private readonly DbSet<Comment> _comments;
    private readonly UserManager<User> _userManager;
    public CommentRepository(AppDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _comments = context.Comments;
        _userManager = userManager;
    }

    public async Task<CommentViewModel> CreateAsync(Comment comment)
    {
        await _comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return Map(comment);
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _comments.FindAsync(id);
        if (comment == null) return null;
        _comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
        
    }

    public async Task<IEnumerable<Comment>> GetByCommentAsync(int parentId)
    {
        return await _comments
            .AsNoTracking()
            .Where(c => c.ParentCommentId == parentId)
            .Take(5)
            .ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _comments.FindAsync(id);
    }

    public async Task<IEnumerable<CommentViewModel>> GetByPostAsync(int postId)
    {
        return await _comments
            .AsNoTracking()
            .Where(c => c.PostId == postId)
            .Take(5)
            .Select(c => Map(c))
            .ToListAsync();
    }

    private CommentViewModel Map(Comment comment)
    {
        return new CommentViewModel
        {
            CommentId = comment.Id,
            UserId = comment.UserId,
            Text = comment.Text,
            ParentCommentId = comment.ParentCommentId,
            Likes = comment.LikedByUsers.Count,
            PostId = comment.PostId
        };
    }
}
