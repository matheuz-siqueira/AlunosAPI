using AlunosAPI.Models;
using AlunosAPI.DTOs.Student;

namespace AlunosAPI.Services;
public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(int id);
    Task<IEnumerable<Student>> GetByNameAsync(string name);
    Task RegisterAsync(RegisterStudentRequestJson request);
    Task UpdateAsync(UpdateStudentRequestJson request, int id);
    Task DeleteAsync(int id);
}
