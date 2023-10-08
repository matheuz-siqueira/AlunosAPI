using AlunosAPI.Models;

namespace AlunosAPI.Repositories;
public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student> GetByIdAsync(int id);
    Task<Student> GetByIdTracking(int id);
    Task<IEnumerable<Student>> GetByNameAsync(string name);
    Task RegisterAsync(Student student);
    Task DeleteAsync(Student student);
    Task UpdateAsync();

}
