using AlunosAPI.DTOs.Student;
using AlunosAPI.Models;

using AutoMapper;

namespace AlunosAPI.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterStudentRequestJson, Student>();
    }
    private void EntityToResponse()
    {
        CreateMap<Student, StudentResponseJson>();
    }
}
