namespace API.Models.DTOs;

public class CommentViewModel
{
    public required int CommentId { get; set; }
    public required string UserId { get; set; }
    public required int PostId { get; set; }
    public int? ParentCommentId { get; set; } = null;
    public required string Text { get; set; }
    public required int Likes { get; set; }
    public required int CommentsQuantity { get; set; }
}
