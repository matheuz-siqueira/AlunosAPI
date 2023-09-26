using Microsoft.EntityFrameworkCore;

using AlunosAPI.Models;

namespace AlunosAPI.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    { }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                Id = 1,
                Name = "User",
                Email = "user@example.com"
            },
            new Student
            {
                Id = 2,
                Name = "Name",
                Email = "name@example.com"
            }
        );
    }
}
