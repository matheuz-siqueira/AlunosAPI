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

    public async Task<IEnumerable<StudentResponseJson>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        var response = _mapper.Map<List<StudentResponseJson>>(students);
        return response;
    }

    public async Task<StudentResponseJson> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student is null)
        {
            throw new StudentNotFoundException("student not found");
        }
        var resposne = _mapper.Map<StudentResponseJson>(student);
        return resposne;
    }

    public async Task<IEnumerable<StudentResponseJson>> GetByNameAsync(GetStudentsRequestJson request)
    {
        IEnumerable<Student> students;
        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            students = await _repository.GetByNameAsync(request.Name);
        }
        else
        {
            students = await _repository.GetAllAsync();
        }
        var response = _mapper.Map<List<StudentResponseJson>>(students);
        return response;
    }

    public async Task<StudentResponseJson> RegisterAsync(RegisterStudentRequestJson request)
    {
        var model = _mapper.Map<Student>(request);
        await _repository.RegisterAsync(model);
        var response = _mapper.Map<StudentResponseJson>(model);
        return response;
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
