using API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace API.Models.DTOs;

public class PostViewModel
{
    public required UserViewModel User { get; set; }
    public required string Text { get; set; }
    public required int LikeAmount { get; set; } 

}
