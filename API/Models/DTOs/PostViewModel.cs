using API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace API.Models.DTOs;

public class PostViewModel
{
    public required int PostId { get; set; }
    public required UserViewModel User { get; set; }
    public required string Text { get; set; }
    public IEnumerable<UserViewModel> LikedByUsers { get; set; } = new List<UserViewModel>();
    public required int Likes { get; set; } = 0;
    public bool UserLiked { get; set; } = false;

}
