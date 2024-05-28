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
    public User User { get; set; }
    public required string UserId { get; set; }
    public required string Text { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public ICollection<User> LikedByUsers { get; set; } = new List<User>();
    public ICollection<Comment> Comments { get; set;  } = new List<Comment>();
}
