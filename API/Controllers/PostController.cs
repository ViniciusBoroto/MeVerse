using API.Data.Repositories;
using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IGenericRepository<Post> _repo;
    public PostController(IGenericRepository<Post> repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        var posts = await _repo.GetAllAsync();
        return Ok(posts);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetById(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> Create(string text)
    {
        if (text == String.Empty)
            return BadRequest();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Post post = new Post { UserId = userId, Text = text };
        _ = await _repo.CreateAsync(post);
        return Ok(post);
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Post>> Delete(int id)
    {
        return await _repo.DeleteAsync(id);
    }
  
}
