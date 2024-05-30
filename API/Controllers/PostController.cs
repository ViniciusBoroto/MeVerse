using API.Data.Repositories;
using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostRepository _repo;
    private readonly IMapper _mapper;
    public PostController(IPostRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        var posts = await _repo.GetAllAsync();
        return Ok(posts.Select(p => _mapper.Map<Post, PostViewModel>(p)));
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Post>> GetById(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        return Ok(_mapper.Map<Post, PostViewModel>(post));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> Create(string text)
    {
        if (text == String.Empty)
            return BadRequest();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var post = await _repo.CreateAsync(userId, text);
        return Ok(_mapper.Map<Post, PostViewModel>(post));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Post>> Delete(int id)
    {
        return Ok(_mapper.Map<Post, PostViewModel>(await _repo.DeleteAsync(id)));
    }

    [Authorize]
    [HttpPost("like/{id:int}")]
    public async Task<ActionResult<Post>> AddLike(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        bool added = await _repo.AddLikeAsync(post, userId);
        if (added) return Ok();
        return Ok(_mapper.Map<Post, PostViewModel>(post));
    }
  
}
