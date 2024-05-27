using API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace API.Models.DTOs;

public class PostDTO
{
    public required string UserId { get; set; }
    public required string Text { get; set; }

}
