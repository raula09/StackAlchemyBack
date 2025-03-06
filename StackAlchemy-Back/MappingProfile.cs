using AutoMapper;
using StackAlchemy_Back.Models;
using StackAlchemy_Back.Models.DTO;
namespace StackAlchemy_Back
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();

            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();

        }
    }
}
