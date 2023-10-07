using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using AlunosAPI.Models;

namespace AlunosAPI.Data;

public class Context : IdentityDbContext<IdentityUser>
{
    public Context(DbContextOptions<Context> options) : base(options)
    { }

    public DbSet<Student> Students { get; set; }
    
}
