using API.Data.Repositories;
using API.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    public PostController(IGenericRepository)
    {
        
    }

    [HttpPost]
    public async Task<ActionResult<Post>> Create(Post post)
    {
        throw
    }
  
}
