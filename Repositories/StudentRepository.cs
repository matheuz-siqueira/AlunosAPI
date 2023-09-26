using AlunosAPI.Data;
using AlunosAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Repositories;

public class StudentRepository
{
    private readonly Context _context;
    public StudentRepository(Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.AsNoTracking()
            .ToListAsync();
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        return await _context.Students.AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> GetByIdTracking(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<Student>> GetByNameAsync(string name)
    {
        return await _context.Students.AsNoTracking()
            .Where(s => s.Name.Contains(name)).ToListAsync();
    }

    public async Task RegisterAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync();
    }
}
