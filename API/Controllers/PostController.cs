using API.Data.Repositories;
using API.Interfaces;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;
[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostRepository _repo;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public PostController(IPostRepository repo,UserManager<User> userManager, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
        _userManager = userManager;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAll()
    {
        var posts = await _repo.GetAllAsync();
        var user = await GetUser();
        var postsDTO = posts.Select(p =>
        {
            var post = mapPost(p);
            if (p.LikedByUsers.Contains(user))
                post.Liked = true;
            return post;
        });
        return Ok(posts);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostViewModel>> GetById(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        if (post is null)
        {
            return NotFound();
        }
        var postDTO = mapPost(post);
        if (post.LikedByUsers.Contains(await GetUser()))
            postDTO.Liked = true;
        return Ok(postDTO);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> Create(string text)
    {
        if (text == String.Empty)
            return BadRequest();
        var user = await GetUser();
        var post = new Post { User = user, UserId = user.Id, Text = text };
        post = await _repo.CreateAsync(post);
        return Ok(mapPost(post));
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Post>> Delete(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        if ( post is null)
        {
            return NotFound();
        }
        return Ok(mapPost(await _repo.DeleteAsync(post)));
    }

    [Authorize]
    [HttpPost("like/{id:int}")]
    public async Task<ActionResult<Post>> Like(int id)
    {
        var post = await _repo.GetByIdAsync(id);
        var user = await GetUser();
        post = await _repo.LikeAsync(post, user);
        return Ok(mapPost(post));
    }

    private async Task<User?> GetUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return await _userManager.FindByIdAsync(userId);
    }
    private PostViewModel mapPost(Post post)
    {
        return _mapper.Map<Post, PostViewModel>(post);
    }
}
