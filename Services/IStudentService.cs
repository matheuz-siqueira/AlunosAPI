using AlunosAPI.Models;
using AlunosAPI.DTOs.Student;

namespace AlunosAPI.Services;
public interface IStudentService
{
    Task<IEnumerable<StudentResponseJson>> GetAllAsync();
    Task<StudentResponseJson> GetByIdAsync(int id);
    Task<IEnumerable<StudentResponseJson>> GetByNameAsync(GetStudentsRequestJson name);
    Task<StudentResponseJson> RegisterAsync(RegisterStudentRequestJson request);
    Task UpdateAsync(UpdateStudentRequestJson request, int id);
    Task DeleteAsync(int id);
}
