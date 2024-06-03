using API.Data.Repositories;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/posts/{postId:int}/comments")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _repo;
    public CommentsController(ICommentRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Comment>> GetById(int id)
    {
        return Ok(await _repo.GetByIdAsync(id));
    }

    [HttpGet("")]
    public async Task<ActionResult>
}
