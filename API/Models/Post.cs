namespace API.Models;

public class Post
{
    public Post()
    {
        
    }
    public Post(string userId, string text)
    {
        UserId = userId;
        Text = text;
    }
    public int PostId { get; set; }
    public User? User { get; set; }
    public required string UserId { get; set; }
    public required string Text { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public ICollection<string> LikedByUserIds { get; set; } = new List<string>();
    public ICollection<int> CommentIds { get; set;  } = new List<int>();
}
