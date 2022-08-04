using AutoMapper;
using RestAPI.Models;

namespace RestAPI.Mappings
{
    public class ToDoProfile : Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDoModel, ToDoViewModel>()
                .ForMember(dest => dest.Todo_title, opt => opt.MapFrom(src => src.Title));
        }
    }
}
