using API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace API.Data.DTOs;

public class PostDTO
{
    public required User User { get; set; }
    public required string Text { get; set; }
    
}
