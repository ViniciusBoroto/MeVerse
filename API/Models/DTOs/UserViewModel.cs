namespace API.Models.DTOs;

public class UserViewModel
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public string? ProfileImagePath { get; set; }

}
