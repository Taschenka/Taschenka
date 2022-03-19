using AutoMapper;
using Taschenka.Dtos;
using Taschenka.Entities;

namespace Taschenka.MapperProfiles;

public class TodoProfile : Profile
{
    public TodoProfile()
    {
        CreateMap<Todo, GetTodoDto>();

        CreateMap<CreateTodoDto, Todo>()
            .ForMember(todo => todo.Id, opt => opt.MapFrom(dto => Guid.NewGuid()));

        CreateMap<UpdateTodoDto, Todo>();
    }
}