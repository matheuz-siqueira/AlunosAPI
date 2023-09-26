using AlunosAPI.DTOs.Student;
using AlunosAPI.Models;

using AutoMapper;

namespace AlunosAPI.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestToEntity();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterStudentRequestJson, Student>();
    }
}
