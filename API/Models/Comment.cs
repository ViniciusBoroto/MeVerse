namespace API.Models;

public class Comment
{
    public int Id { get; set; }
    public required User User { get; set; }
    public required string UserId { get; set; }
    public required string Text { get; set; }
    public required Post Post { get; set; }
    public required int PostId { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<User> LikedByUsers { get; set; } = new List<User>();
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    public DateTime CreationDate { get; set; } = DateTime.Now;
}
