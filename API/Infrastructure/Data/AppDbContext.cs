using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data;
internal class AppDbContext : IdentityDbContext<User>
{

}
