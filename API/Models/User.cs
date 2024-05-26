using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class User : IdentityUser
{
    public string? ProfileImagePath { get; set; } = null;
    public string Biography { get; set; } = String.Empty;
    public ICollection<User> Followers { get; set; } = new List<User>();
    public ICollection<User> Following { get; set; } = new List<User>();
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Post> LikedPosts { get; set; } = new List<Post>();
    public ICollection<Comment> LikedComments { get; set; } = new List<Comment>();

}
