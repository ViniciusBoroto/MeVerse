using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _repo;
    private readonly UserManager<User> _userManager;
    public PostsController(IPostRepository repo,UserManager<User> userManager)
    {
        _repo = repo;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {  
        return Ok(await _repo.GetAllAsync(GetUserId()));
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostViewModel>> GetById(int id)
    {
        var post = await _repo.GetByIdAsync(id, GetUserId());
        if (post is null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> Create(string text)
    {
        if (text == String.Empty)
            return BadRequest();
        var user = await GetUser();
        var post = new Post { User = user, UserId = user.Id, Text = text };
        return Ok(await _repo.CreateAsync(post));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Post>> Delete(int id)
    {
        var post = await _repo.DeleteAsync(id);
        if ( post is null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [Authorize]
    [HttpPost("like/{id:int}")]
    public async Task<ActionResult<Post>> Like(int id)
    {
        var post = await _repo.LikeAsync(id, await GetUser());
        return Ok(post);
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    private async Task<User?> GetUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId);
    }
}
