using AlunosAPI.Models;
using AlunosAPI.DTOs.Student;
using AlunosAPI.Exceptions;
using AlunosAPI.Repositories;

using AutoMapper;

namespace AlunosAPI.Services;

public class StudentService : IStudentService
{
    private readonly StudentRepository _repository;
    private readonly IMapper _mapper;
    public StudentService(StudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        var response = await _repository.GetAllAsync();
        return response;
    }

    public async Task<Student> GetByIdAsync(int id)
    {
        var response = await _repository.GetByIdAsync(id);
        return response;
    }

    public async Task<IEnumerable<Student>> GetByNameAsync(string name)
    {
        IEnumerable<Student> response;
        if (!string.IsNullOrWhiteSpace(name))
        {
            response = await _repository.GetByNameAsync(name);
        }
        else
        {
            response = await _repository.GetAllAsync();
        }
        return response;
    }

    public async Task RegisterAsync(RegisterStudentRequestJson request)
    {
        var model = _mapper.Map<Student>(request);
        await _repository.RegisterAsync(model);
    }

    public async Task UpdateAsync(UpdateStudentRequestJson request, int id)
    {
        var model = await _repository.GetByIdTracking(id);
        if (model is null)
        {
            throw new StudentNotFoundException("student not found");
        }
        _mapper.Map(request, model);
        await _repository.UpdateAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model is null)
        {
            throw new StudentNotFoundException("student not found");
        }
        await _repository.DeleteAsync(model);
    }
}
